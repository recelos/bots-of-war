using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet, _bot;
    [SerializeField] private Transform _centralPoint, _shootPoint;
    private BasicMovement _basicMovement;
    private float _fireRate;
    private float _timeSinceLastShot;
    private Vector2 _direction, _mousePos;
    private int _damage;
    void Start()
    {
        //szybkostrzelnosc (500 pociskow na minute)
        _fireRate = 500;
        _basicMovement =  _bot.GetComponent<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //zczytanie pozycji myszy
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //obliczenie kierunku w ktory ma byc obrocony central point
        _direction = _mousePos - (Vector2) _centralPoint.position;
        //obrocenie central pointa o kat
        
        if(_basicMovement.isFacingRight()){
            _centralPoint.transform.right = _direction;
        }else{
             _centralPoint.transform.right = -_direction;
        }
        

        //ile czasu uplynelo od ostatniego strzlu
        _timeSinceLastShot += Time.deltaTime;

        if(Input.GetMouseButton(0) && CanShoot()){
            Shoot();
            _timeSinceLastShot = 0;
        }
        
    }

    private void Shoot(){
        //stworzenie pocisku w miejsce wystrzalu z obrotem wystrzalu
        GameObject bullet = Instantiate(_bullet,_shootPoint.position, _shootPoint.rotation);
        //nadanie kuli predkosci o zhardkodowana wartosc
        if(_basicMovement.isFacingRight())
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * 400);
        else
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * (-400));
        //ustawienie obrazen pocisku
        bullet.GetComponent<Bullet>().setDamage(_damage);
      
    }
    //sprawdza czy uplynelo wystarczajaca czasu od poprzedniego strzalu
    private bool CanShoot(){
        return _timeSinceLastShot > 1f / (_fireRate / 60f);
    }
}
