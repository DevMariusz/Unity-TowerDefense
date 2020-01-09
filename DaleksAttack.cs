//deathray delay fire!!!!!!!!!!!!
private float fireCountdown = 3f;

// daleks fight back at shooting turrets!!!
Enemy e = target.GetComponent<Enemy>();
if (e != null)
  e.defenseFire(transform, thisTurret);

//turn Dalek
Vector3 dir2 = target.position - transform.position;
Quaternion lookRotation = Quaternion.LookRotation(dir2);
Vector3 rotation = lookRotation.eulerAngles;
enemy.dalekRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

// daleks fight back at shooting turrets!!!!!!!!!!!!!!!!!!!!!
public void defenseFire(Transform turretLocation, GameObject currentTurrent)
{
    if (chargeSoundplaying == false)
    {
    //play sound
        sounds.playSound("Dalek_Weapon_charge");
        chargeSoundplaying = true;
    }

    if(turretLocation != turretlocationcheck)
    {
        turretlocationcheck = turretLocation;
        currentTurret_obj = currentTurrent;

        //deathray delay fire!!!!!!!!!!!!
        fireCountdown = 3f;
    }

    childObj.gameObject.SetActive(true);
    Vector3 dir2 = turretLocation.position - transform.position;
    Quaternion lookRotation = Quaternion.LookRotation(dir2);
    Vector3 rotation = lookRotation.eulerAngles;
    dalekRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    if (fireCountdown <= 0f)
    {
    dalekShoot(turretLocation);
        fireCountdown = 1f / fireRate;
    }
    fireCountdown -= Time.deltaTime;
}

void dalekShoot(Transform turretLocation)
{
    bulletGO = (GameObject)Instantiate(weaponPreFab, transform.position, dalekRotate.rotation);
    Bullet_enemy bullet = bulletGO.GetComponent<Bullet_enemy>();
    if (bullet != null)
    {
        //play sound
        if (gameObject.name == "Dalek(Clone)")
        {
            sounds.playSound("Dalek_say_Destroy");
        }
        sounds.playSound("Dalek_shoots");
        bullet.Seek(turretLocation, currentTurret_obj);
        childObj.gameObject.SetActive(false);
    }
}

//Daleks hit TRADIS
if (wavepointIndex >= Waypoints.points.Length - 1)
{
  //play sound
  sounds.playSound("Dalek_say_Exterminate");
  EndPath();
  return;
}


void Explode()
{
  // this would be awesome in my space shooter game!! explode enemys around the guy that
  //just got shot
  Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
  foreach (Collider collider in colliders)
      if(collider.tag == "Enemy")
          Damage(collider.transform, null);
}
