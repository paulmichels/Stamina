using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGroup
{
    static public List<IEnemy> CreateGroup()
    {
        int random = Random.Range(1, 101);
        if(random >= 1 && random <= 4)
        {
            return Group(new Quadrature());
        }
        else if (random >= 5 && random <= 6)
        {
            return Group(new Quadrature(), new Quadrature());
        }
        else if (random >= 7 && random <= 12)
        {
            return Group(new Quadrature(), new Astichaut(), new Astichaut());
        }
        else if (random >= 13 && random <= 16)
        {
            return Group(new Quadrature(), new Netflex());
        }
        else if (random >= 17 && random <= 18)
        {
            return Group(new Quadrature(), new Astichaut(), new Astichaut(), new Netflex());
        }
        else if (random == 19)
        {
            return Group(new Quadrature(), new Quadrature(), new Netflex(), new Netflex());
        }
        else if (random == 20)
        {
            return Group(new Quadrature(), new DixHuit());
        }
        else if (random >= 21 && random <= 25)
        {
            return Group(new Quadrature(), new Sarkasme(), new Astichaut());
        }
        else if (random >= 26 && random <= 35)
        {
            return Group(new Astichaut(), new Astichaut());
        }
        else if (random >= 36 && random <= 45)
        {
            return Group(new Astichaut(), new Astichaut(), new Fhlaime());
        }
        else if (random >= 46 && random <= 52)
        {
            return Group(new Astichaut(), new Astichaut(), new Astichaut());
        }
        else if (random >= 53 && random <= 55)
        {
            return Group(new Astichaut(), new Astichaut(), new Astichaut(), new Astichaut());
        }
        else if (random >= 56 && random <= 60)
        {
            return Group(new Netflex());
        }
        else if (random >= 61 && random <= 65)
        {
            return Group(new Astichaut(), new Astichaut(), new Netflex());
        }
        else if (random >= 66 && random <= 70)
        {
            return Group(new Fhlaime(), new Fhlaime());
        }
        else if (random >= 71 && random <= 75)
        {
            return Group(new Fhlaime(), new Fhlaime(), new Fhlaime());
        }
        else if (random >= 76 && random <= 80)
        {
            return Group(new DixHuit(), new Astichaut(), new Astichaut());
        }
        else if (random >= 81 && random <= 85)
        {
            return Group(new DixHuit(), new Sarkasme());
        }
        else if (random >= 86 && random <= 90)
        {
            return Group(new DixHuit(), new Fhlaime(), new Fhlaime());
        }
        else if (random >= 91 && random <= 95)
        {
            return Group(new Sarkasme());
        }
        else
        {
            return Group(new Sarkasme(), new Sarkasme());
        }
    }

    static private List<IEnemy> Group(IEnemy enemy1, IEnemy enemy2 = null, IEnemy enemy3 = null, IEnemy enemy4 = null)
    {
        List<IEnemy> enemies = new List<IEnemy>();
        enemies.Insert(0, enemy1);
        enemies.Insert(1, enemy2);
        enemies.Insert(2, enemy3);
        enemies.Insert(3, enemy4);
        return enemies;
    }
}
