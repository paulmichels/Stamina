using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGroup
{
    static public List<IEnemy> CreateGroup()
    {
        EnemySpriteSelector enemySpriteSelector = GameObject.Find("Middleground").GetComponent<EnemySpriteSelector>();

        int random = Random.Range(1, 101);
        if(random >= 1 && random <= 4)
        {
            return Group(new Quadrature(enemySpriteSelector));
        }
        else if (random >= 5 && random <= 6)
        {
            return Group(new Quadrature(enemySpriteSelector), new Quadrature(enemySpriteSelector));
        }
        else if (random >= 7 && random <= 12)
        {
            return Group(new Quadrature(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector));
        }
        else if (random >= 13 && random <= 16)
        {
            return Group(new Quadrature(enemySpriteSelector), new Netflex(enemySpriteSelector));
        }
        else if (random >= 17 && random <= 18)
        {
            return Group(new Quadrature(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Netflex(enemySpriteSelector));
        }
        else if (random == 19)
        {
            return Group(new Quadrature(enemySpriteSelector), new Quadrature(enemySpriteSelector), new Netflex(enemySpriteSelector), new Netflex(enemySpriteSelector));
        }
        else if (random == 20)
        {
            return Group(new Quadrature(enemySpriteSelector), new DixHuit(enemySpriteSelector));
        }
        else if (random >= 21 && random <= 25)
        {
            return Group(new Quadrature(enemySpriteSelector), new Sarkasme(enemySpriteSelector), new Astichaut(enemySpriteSelector));
        }
        else if (random >= 26 && random <= 35)
        {
            return Group(new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector));
        }
        else if (random >= 36 && random <= 45)
        {
            return Group(new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Fhlaime(enemySpriteSelector));
        }
        else if (random >= 46 && random <= 52)
        {
            return Group(new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector));
        }
        else if (random >= 53 && random <= 55)
        {
            return Group(new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector));
        }
        else if (random >= 56 && random <= 60)
        {
            return Group(new Netflex(enemySpriteSelector));
        }
        else if (random >= 61 && random <= 65)
        {
            return Group(new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Netflex(enemySpriteSelector));
        }
        else if (random >= 66 && random <= 70)
        {
            return Group(new Fhlaime(enemySpriteSelector), new Fhlaime(enemySpriteSelector));
        }
        else if (random >= 71 && random <= 75)
        {
            return Group(new Fhlaime(enemySpriteSelector), new Fhlaime(enemySpriteSelector), new Fhlaime(enemySpriteSelector));
        }
        else if (random >= 76 && random <= 80)
        {
            return Group(new DixHuit(enemySpriteSelector), new Astichaut(enemySpriteSelector), new Astichaut(enemySpriteSelector));
        }
        else if (random >= 81 && random <= 85)
        {
            return Group(new DixHuit(enemySpriteSelector), new Sarkasme(enemySpriteSelector));
        }
        else if (random >= 86 && random <= 90)
        {
            return Group(new DixHuit(enemySpriteSelector), new Fhlaime(enemySpriteSelector), new Fhlaime(enemySpriteSelector));
        }
        else if (random >= 91 && random <= 95)
        {
            return Group(new Sarkasme(enemySpriteSelector));
        }
        else
        {
            return Group(new Sarkasme(enemySpriteSelector), new Sarkasme(enemySpriteSelector));
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
