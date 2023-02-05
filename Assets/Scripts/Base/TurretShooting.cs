
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public enum ShotStatus
{
    Normal,
    Empty
}

public class TurretShooting : MonoBehaviour, IProjectileShooter
{
    public event Action<ShotStatus> Shot;
    
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject rangeSpriteGameObject;
    [SerializeField] private AudioSource strzalSoundEffect;
    [SerializeField] private AudioSource reloadSoundEffect;

    [SerializeField, Range(1f, 50f)] private float shootingRange;

    public float Range => shootingRange;
    public Vector2 ShotPoint => transform.position;
    public Vector2 ShotDirection => movement.TurretDirection;
    public Origin ShotOrigin => Origin.Player;

    private TurretMovement movement;
    private Turret turret;

    private ProjectileItem currentProjectileItem;

    private float elapsedTime;

    private int shotsShot;

    private bool CanShoot() => shotsShot < currentProjectileItem.shootsAmount;

    private ParticleSystem particleChmurka;
    
    private void Awake()
    {
        particleChmurka = GetComponent<ParticleSystem>();
        movement = GetComponent<TurretMovement>();
        turret = GetComponent<Turret>();
        turret.InteractionStateChanged += OnInteractionStateChanged;
        rangeSpriteGameObject.transform.localScale = Vector3.one * shootingRange * 2;
        rangeSpriteGameObject.SetActive(false);
    }

    private void OnInteractionStateChanged(bool isInteracting)
    {
        rangeSpriteGameObject.SetActive(isInteracting);
        if (!isInteracting) return;
        currentProjectileItem = EquipmentController.Instance.CurrentItem as ProjectileItem;
        EquipmentController.Instance.PutDownItem();
        shotsShot = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && turret.IsUsed && elapsedTime > cooldown)
        {
            if (CanShoot())
            {
                strzalSoundEffect.Play();
                Shoot();
            }
            else
            {
                reloadSoundEffect.Play();
                Shot?.Invoke(ShotStatus.Empty);
            }
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }

    private void Shoot()
    {
        Projectile.Spawn(this, currentProjectileItem, shootingRange);
        shotsShot++;
        Shot?.Invoke(ShotStatus.Normal);
        particleChmurka.Play();
    }
}