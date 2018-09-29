using UnityEngine;
using System;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    //public string name;

    private bool i_fire;
    private float i_x;
    private float i_y;
    private Vector3 i_moveVector;
    private Vector3 moveVelocity;
    private Vector3 i_mousePosition;
    private Vector2 dirFacing;
    public float moveSpeed;

    public string equippedWeapon;
    public Weapon weapon;

    private Camera playerCamera;
    private Rigidbody2D playerRigidbody;

    public Sprite headBlue;
    public GameObject PlayerHead;
    public GameObject PlayerTorso;
    public GameObject weaponRifle;
    public GameObject weaponShotgun;
    public GameObject weaponSMG;

    // Use this for initialization
    void Start () {
        equippedWeapon = "";
        weapon = new Weapon(Type.unarmed, 0, 0, 0);
        if (isLocalPlayer) {
            InitializeVariables();
        }
	}

    private void InitializeVariables() {
        playerCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (weapon.ToString() != equippedWeapon && !equippedWeapon.Equals(""))
            localChangeWeapon((Type)Enum.Parse(typeof(Type), equippedWeapon));
        if (isLocalPlayer) {
            GetInput();
            if (i_fire && weapon != null) {
                CmdCheckFire();
            }
        }
    }

    void FixedUpdate() {
        if (isLocalPlayer) {
            Move();
            CameraControl();
        }
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        PlayerHead.GetComponent<SpriteRenderer>().sprite = headBlue;
    }

    private void GetInput() {
        i_x = Input.GetAxis("Horizontal");
        i_y = Input.GetAxis("Vertical");
        i_moveVector = new Vector3(i_x, i_y);
        moveVelocity = i_moveVector * moveSpeed;

        i_fire = (Input.GetAxis("Fire") > 0.01f && !i_fire)?true:false;
        i_mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        dirFacing = new Vector2(i_mousePosition.x - transform.position.x, i_mousePosition.y - transform.position.y);
    }

    [Command]
    private void CmdCheckFire() {
        GameObject rawBullet = weapon.Fire(this.gameObject);

        if (rawBullet != null) {
            GameObject bullet = Instantiate(rawBullet);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * weapon.bulletSpeed;

            NetworkServer.Spawn(bullet);

            Destroy(bullet, 2);
        }
    }

    private void Move() {
        playerRigidbody.transform.up = dirFacing;
        if (Vector3.Dot(dirFacing, moveVelocity) >0) {
            playerRigidbody.velocity = moveVelocity;
        }
        else {
            playerRigidbody.velocity = moveVelocity/2;
        }

    }

    [Command]
    public void CmdChangeWeapon(Type type) {
        localChangeWeapon(type);
    }

    [ClientRpc]
    public void RpcChangeWeapon(Type type) {
        localChangeWeapon(type);
    }

    private void localChangeWeapon(Type type) {
        weaponRifle.SetActive(false);
        weaponShotgun.SetActive(false);
        weaponSMG.SetActive(false);

        try {
            switch (weapon.type) {
                case Type.rifle:
                    weaponRifle.SetActive(true);
                    break;
                case Type.shotgun:
                    weaponShotgun.SetActive(true);
                    break;
                case Type.smg:
                    weaponSMG.SetActive(true);
                    break;
                case Type.unarmed:
                    break;
                default:
                    break;
            }
            equippedWeapon = type.ToString();
        }
        catch {
            Debug.Log("Error: Can't change a null weapon.");
        }
    }

    public void ChangeWeapon(Type type) {
        if (isLocalPlayer) {
            CmdChangeWeapon(type);
        }
        else if (isServer) {
            RpcChangeWeapon(type);
        }
        localChangeWeapon(type);
    }
    

    private void CameraControl() {
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "DroppedWeapon") {
            weapon = collision.gameObject.GetComponent<WeaponDropped>().GetWeapon();
            ChangeWeapon(collision.gameObject.GetComponent<WeaponDropped>().GetWeaponType());
            Destroy(collision.gameObject);
        }
    }
}
