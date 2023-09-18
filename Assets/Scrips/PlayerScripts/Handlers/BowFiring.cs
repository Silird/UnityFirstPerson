using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using System;

public class BowFiring : ToolHandler
{
    private bool _startAim;
    private float _sightSize;

    private Bow _equippedBow;

    //Объект камеры
    private Camera _camera;

    private Animator _animator;
    private InventoryManager _inventoryManager;

    Rig _rigAim;
    
    private static readonly int AnimAim = Animator.StringToHash("aim");

    private void Awake()
    {
        InventoryManager.ToolChanged += tool =>
        {
            if (tool.GetType() == typeof(Bow))
            {
                _equippedBow = tool as Bow;
            }
        };
    }

    private void Start()
    {
        //Получаем данные о камере
        _camera = Camera.main;

        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.Log("Animator not Found");
        }
        _inventoryManager = GetComponentInChildren<InventoryManager>();
        if (_inventoryManager == null)
        {
            Debug.Log("InventoryManager not Found");
        }

        _rigAim = Util.GetChildComponentByName<Rig>(this, "RigAim");
        if (_inventoryManager == null)
        {
            Debug.Log("RigAim not Found");
        }
    }

    public override void Mouse()
    {
        Aim();
    }

    public override void MouseUp()
    {
        Hit();
    }

    private void Aim()
    {
        if (_startAim)
        {
            // Изменение прицела
            _sightSize -= _equippedBow.aimSpeed * Time.deltaTime;
            _sightSize = Mathf.Max(_sightSize, _equippedBow.sightMin);
        }
        else
        {
            _inventoryManager.EquipItem("Arrow");
            AudioManager.Instance.Play("Arrow Pullback", gameObject);

            // Старт анимации
            _animator.SetBool(AnimAim, true);

            _startAim = true;

            // Старт прицела
            _sightSize = _equippedBow.sightMax;
        }
        // Применение нового прицела
        UIManager.Instance.SetSightSize(_sightSize);
    }

    private void Hit()
    {
        var ray = GetHitRay();

        //Если попали в какой то объект
        if (Physics.Raycast(ray, out var hit))//пускаем луч ray результат столкновения считываем в hit
        {
            //Распознавание попаданий в цель
            // GameObject hitObject = hit.transform.gameObject;//получаем объект, в который попали

            //запускаем сопрограмму
            StartCoroutine(SphereIndicatorCoroutine(hit, ray));
            //рисуем отладочную линию, чтобы проследить траекторию луча
            Debug.DrawLine(this.transform.position, hit.point, Color.green, 2);

        }

        _startAim = false;
        _animator.SetBool(AnimAim, false);
        ResetAimRig();
        _sightSize = _equippedBow.sightMin;
        UIManager.Instance.SetSightSize(_sightSize);
    }

    private void ResetAimRig()
    {
        _rigAim.weight = 0;
    }

    //Сопрограмма, которая отрабатывает попадание
    private static IEnumerator SphereIndicatorCoroutine(RaycastHit hit, Ray ray)
    {
        //Создаем игровой объект сферу
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        AudioManager.Instance.Play("Arrow Hit", sphere);
        sphere.transform.position = hit.point;//указываем позицию сферы

        var effect = Instantiate(EffectManager.Instance.Get("HitEffect").gameObject);
        effect.transform.position = hit.point;
        effect.transform.forward = -ray.direction;
        effect.GetComponent<ParticleSystem>().Emit(1);

        //yield - ключевое слово для сопрограммы, которое указывает ей, что пора остановиться
        yield return new WaitForSeconds(6);//время ожидания

        //После ожидания вернется в эту часть сопрограммы
        Destroy(sphere);//удалим сферу
        Destroy(effect);//удалим эффект
    }

    private Ray GetHitRay()
    {
        var rayOrigin = _camera.transform.position;


        var radius = Util.GaussRandom() * _sightSize;
        var angle = (float)(2 * Math.PI * Util.Rand.NextDouble());

        var x = radius * (float)Math.Cos(angle) * 0.001f;
        var y = radius * (float)Math.Sin(angle) * 0.001f;

        var rayDestination = Util.TransformRelative(_camera.transform, new Vector3(x, y, 1));

        // Debug.DrawLine(rayOrigin, rayDestination, Color.green, 1);
        var result = new Ray
        {
            origin = rayOrigin,
            direction = rayDestination - rayOrigin
        };

        return result;
    }
}
