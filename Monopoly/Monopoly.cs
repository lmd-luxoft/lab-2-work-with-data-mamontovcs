using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class Monopoly
    {
        public List<Player> players = new List<Player>();
        public List<Field> fields = new List<Field>();

        public Monopoly(string[] playersNames)
        {
            for (int i = 0; i < playersNames.Length; i++)
            {
                players.Add(new Player() { Name = playersNames[i], Money = 6000 });
            }

            fields.Add(new Field { Name = "Ford", Type = Type.AUTO, Price = 0, IsBought = false });
            fields.Add(new Field { Name = "MCDonald", Type = Type.FOOD, Price = 0, IsBought = false });

            fields.Add(new Field { Name = "Lamoda", Type = Type.CLOTHER, Price = 0, IsBought = false });
            fields.Add(new Field { Name = "Air Baltic", Type = Type.TRAVEL, Price = 0, IsBought = false });
            fields.Add(new Field { Name = "Nordavia", Type = Type.TRAVEL, Price = 0, IsBought = false });
            fields.Add(new Field { Name = "Prison", Type = Type.PRISON, Price = 0, IsBought = false });
            fields.Add(new Field { Name = "MCDonald", Type = Type.FOOD, Price = 0, IsBought = false });
            fields.Add(new Field { Name = "TESLA", Type = Type.AUTO, Price = 0, IsBought = false });
        }

        internal List<Player> GetPlayersList()
        {
            return players;
        }


        internal List<Field> GetFieldsList()
        {
            return fields;
        }

        internal Field GetFieldByName(string fieldName)
        {
            return (from p in fields where p.Name == fieldName select p).FirstOrDefault();
        }

        internal bool Buy(int v, Tuple<string, Type, int, bool> k)
        {
            var x = GetPlayerInfo(v);
            int cash = 0;
            switch (k.Item2)
            {
                case Type.AUTO:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 500;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                case Type.FOOD:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 250;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                case Type.TRAVEL:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 700;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                case Type.CLOTHER:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 100;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                default:
                    return false;
            }
            int i = players.Select((item, index) => new { name = item.Item1, index = index })
                .Where(n => n.name == x.Item1)
                .Select(p => p.index).FirstOrDefault();
            fields[i] = new Tuple<string, Type, int, bool>(k.Item1, k.Item2, v, k.Item4);
            return true;
        }

        internal Tuple<string, int> GetPlayerInfo(int v)
        {
            return players[v - 1];
        }

        internal bool Renta(int v, Tuple<string, Type, int, bool> k)
        {
            var z = GetPlayerInfo(v);
            Tuple<string, int> o = null;
            switch (k.Item2)
            {
                case Type.AUTO:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 250);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 250);
                    break;
                case Type.FOOD:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 250);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 250);

                    break;
                case Type.TRAVEL:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 300);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 300);
                    break;
                case Type.CLOTHER:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 100);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 1000);

                    break;
                case Type.PRISON:
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 1000);
                    break;
                case Type.BANK:
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 700);
                    break;
                default:
                    return false;
            }
            players[v - 1] = z;
            if (o != null)
                players[k.Item3 - 1] = o;
            return true;
        }
    }
}
