using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluations
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Start process");


            var options = GenerateOptions();

            var keys = options.Select(op => op.type).Distinct().ToList() ;

            int keytipo = 1;

            Dictionary<string, List<Option>> dicc = new Dictionary<string, List<Option>>();
            foreach(var ky in keys)
            {

                var obPerTipe = options.Where(wh => wh.type.Equals(ky)).OrderByDescending(or=> or.quantity).ThenBy(or => or.price) .ToList();

                obPerTipe.ForEach(s => s.group1eval = keytipo);
                dicc.Add(ky, obPerTipe);
                

                keytipo++;
            }

            PrintTypes(keys);

            Console.WriteLine("Tipo\t Vol\t Precio\t");
            foreach (var ky in dicc.Keys)
            {
                int gr2val = 1;
                foreach (var obj1 in dicc[ky])
                {
                    List<Option> optionsInGroup = new List<Option>();

                    if (obj1.group2order != 0) continue;

                    int indexVal = dicc[ky].IndexOf(obj1);

                    optionsInGroup.Add(obj1);
                    for (int j = (indexVal + 1); j< dicc[ky].Count; j++)
                    {
                        if (dicc[ky].Count > j)
                        {
                            var obj2 = dicc[ky][j];

                            var maxDif = obj1.quantity >= 800000 ? (obj1.quantity * 0.05M) : (obj1.quantity * 0.1M);

                            if (obj1.quantity - obj2.quantity <= maxDif)
                                optionsInGroup.Add(obj2);
                        }
                    }
                    optionsInGroup.ForEach(fe => fe.group2eval = gr2val);
                    gr2val++;
                    EvaluateOrdersByPrice(optionsInGroup);
                    PrintValues(optionsInGroup);
                }
            }

        }


        private static void EvaluateOrdersByPrice(List<Option> opts)
        {
            opts = opts.OrderBy(op => op.price).ToList();

            int ordereval = 1;

            bool flag = false;
            for(int i=0; i<opts.Count - 1 ; i++)
            {
                flag = true;
                int j = i + 1;

                if(j < opts.Count)
                {

                    if(opts[i].price < opts[j].price)
                    {
                        opts[i].group2order = ordereval;
                        ordereval++;
                        opts[j].group2order = ordereval;
                    }
                    else
                    {
                        //same price
                        opts[j].group2order = opts[i].group2order = ordereval;
                    }

                }
                else
                {
                    throw new Exception("out of range");
                }
            }

            if(!flag && opts.Count==1)
            {
                opts[0].group2order = ordereval;
            }
        }

        
        static void PrintTypes(List<string> keys)
        {
            keys.ForEach(ky =>
            {
                Console.WriteLine("Se encontro el tipo: " + ky);
            });
            Console.WriteLine("------------- ");
        }

        static void PrintValues(List<Option> values)
        {
            values.ForEach(val =>
            {
                Console.WriteLine(val.type +"\t "+ val.quantity + "\t " + val.price + "\t " + val.group1eval + "\t " + val.group2eval + "\t " + val.group2order);
            });
            Console.WriteLine("------------- ");
        }

        static List<Option> GenerateOptions()
        {

            List<Option> objs = new List<Option>();

            Option op1 = new Option()
            {
                type = "Black",
                quantity = 750000,
                price = 4
            };
            objs.Add(op1);

            Option op2 = new Option()
            {
                type = "Black",
                quantity = 690000,
                price = 3.8M
            };
            objs.Add(op2);

            Option op3 = new Option()
            {
                type = "Black",
                quantity = 700000,
                price = 4.1M
            };
            objs.Add(op3);

            Option op4 = new Option()
            {
                type = "Black",
                quantity = 680000,
                price = 4.1M
            };
            objs.Add(op4);

            /******************************************/
            Option op10 = new Option()
            {
                type = "White",
                quantity = 750000,
                price = 3
            };
            objs.Add(op10);

            Option op11 = new Option()
            {
                type = "White",
                quantity = 690000,
                price = 3.8M
            };
            objs.Add(op11);

            Option op12 = new Option()
            {
                type = "White",
                quantity = 700000,
                price = 4.1M
            };
            objs.Add(op12);

            Option op13 = new Option()
            {
                type = "White",
                quantity = 680000,
                price = 4.1M
            };
            objs.Add(op13);

            Option op14 = new Option()
            {
                type = "Nemba",
                quantity = 990100,
                price = 3.1M
            };
            objs.Add(op14);

            Option op15 = new Option()
            {
                type = "Nemba",
                quantity = 700160,
                price = 4.1M
            };
            objs.Add(op15);

            /*****************************************/
            Option op16 = new Option()
            {
                type = "BRENT",
                quantity = 395000,
                price = 2M
            };
            objs.Add(op16);

            Option op17 = new Option()
            {
                type = "BRENT",
                quantity = 400100,
                price = 3M
            };
            objs.Add(op17);

            Option op18 = new Option()
            {
                type = "BRENT",
                quantity = 390000,
                price = 3M
            };
            objs.Add(op18);

            Option op19 = new Option()
            {
                type = "BRENT",
                quantity = 397000,
                price = 3.7M
            };
            objs.Add(op19);

            return objs;
        }

    }
}
