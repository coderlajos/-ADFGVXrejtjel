namespace ADFGVX
{
    
    class ADFGVXrejtjel
    {
        private char[,] Kodtabla;
        private string Uzenet;
        private string Kulcs;
        int n = 0;
        public string AtalakitottUzenet()
        {
            // 5. feladat:
            string uzenet = Uzenet.Replace(" ","");
            int uzenet_hossz = Uzenet.Length;
            int kulcs_hossz = Kulcs.Length;
            while (uzenet_hossz%kulcs_hossz!=0)
            {
                uzenet += "x";
                uzenet_hossz = uzenet.Length;
            }
            n = uzenet_hossz;
            return uzenet;
        }

        public string Kodszoveg()
        {
            // 7. feladat:
            string t = "";
            for (int i = 0; i < n; i++)
            {
                t+=Betupar(AtalakitottUzenet()[i]);
            }
            return t;
        }

        public string KodoltUzenet()
        {
            string kodszoveg = Kodszoveg();
            int sorokSzama = kodszoveg.Length / Kulcs.Length;
            int oszlopokSzama = Kulcs.Length;
            char[,] m = new char[sorokSzama, oszlopokSzama];
            int index = 0;
            for (int sor = 0; sor < sorokSzama; sor++)
            {
                for (int oszlop = 0; oszlop < oszlopokSzama; oszlop++)
                {
                    m[sor, oszlop] = kodszoveg[index++];
                }
            }

            string kodoltUzenet = "";
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                int oszlopIndex = Kulcs.IndexOf(ch);
                if (oszlopIndex != -1)
                {
                    for (int sorIndex = 0; sorIndex < sorokSzama; sorIndex++)
                    {
                        kodoltUzenet += m[sorIndex, oszlopIndex];
                    }
                }
            }
            return kodoltUzenet;
        }
        public string Betupar(char k)
        {
            string[] adfgvx = { "A", "D", "F", "G", "V", "X" };
            string hiba = "";
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    if (Kodtabla[i,j]==k)
                    {
                        hiba = adfgvx[i] + adfgvx[j];
                    }
                }
            }
            return hiba;
        }
        public ADFGVXrejtjel(string kodtablaFile, string uzenet, string kulcs)
        {
            Uzenet = uzenet;
            Kulcs = kulcs;

            Kodtabla = new char[6, 6];
            int sorIndex = 0;
            foreach (var sor in System.IO.File.ReadAllLines(kodtablaFile))
            {
                for (int oszlopIndex = 0; oszlopIndex < sor.Length; oszlopIndex++)
                {
                    Kodtabla[sorIndex, oszlopIndex] = sor[oszlopIndex];
                }
                sorIndex++;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2. feladat:");
            Console.Write("\tKérem a kulcsot [HOLD]: ");
            string kulcs = Console.ReadLine().ToUpper();
            Console.Write("\tKérem az üzenetet [ szeretem a csokit ]: ");
            string uzenet = Console.ReadLine().ToLower();
            if (kulcs == "") kulcs = "HOLD";
            if (uzenet == "") uzenet = "szeretem a csokit";
            ADFGVXrejtjel rejtjel = new ADFGVXrejtjel("kodtabla.txt", uzenet, kulcs);
            //5
            Console.Write("5. feladat: Az átlakított üzenet: ");
            Console.Write($"{rejtjel.AtalakitottUzenet()}\n");
            //6
            Console.Write("6. feladat: ");
            Console.Write($"s->{rejtjel.Betupar('s')} ");
            Console.Write($"x->{rejtjel.Betupar('x')}");
            //7
            Console.Write("\n7. feladat: A kódszöveg: ");
            Console.WriteLine($"{rejtjel.Kodszoveg()}");
            //8
            Console.Write("8. feladat: A kódolt üzenet: ");
            Console.Write($"{rejtjel.KodoltUzenet()}");
        }
    }
}
