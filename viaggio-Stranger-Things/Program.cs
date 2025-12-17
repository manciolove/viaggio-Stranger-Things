
using System;

namespace gioco_stranger_things
{

    internal class Program
    {
        static Random r = new Random();

        // funzione dado
        static int TiroDado(int posizione)
        {
            // primo tiro
            if (posizione == 0)
            {
                return r.Next(1, 5);
            }
            // altri tiri 
            else
            {
                return r.Next(1, 7);
            }
        }
        // funzione inventario
        static void Inventario(int armi, int cure, bool cavalcatura)
        {
            Console.WriteLine("inventario:");
            Console.WriteLine("armi: " + armi);
            Console.WriteLine("pozioni curative: " + cure);
            Console.WriteLine("cavalcatura: " + cavalcatura);
        }

        // funzione status
        static void Status(int vita, int resistenza, int dannoBase, int posizione, string[] mappa)
        {

            Console.WriteLine("vita: " + vita);
            Console.WriteLine("resistenza: " + resistenza);
            Console.WriteLine("danno base: " + dannoBase);
            Console.WriteLine("posizione: " + mappa[posizione]);
        }



        // funzione combattimento
        static bool Combatti(ref int vita, ref int resistenza, ref int armi)
        {
            int vitaDemo = 100;
            int dannoGiocatore;

            Console.WriteLine("un Demogorgone emerge dall'ombra, i suoi occhi neri ti fissano senza pietà.");
            Console.WriteLine("tu stringi i denti, pronto a difenderti");
            // muori perche non hai vita e resistenza
            if (vita <= 50 && resistenza < 20)
            {
                Console.WriteLine("oh nooo!!");
                Console.WriteLine("Demogorgone: troppo facile!");
                Console.WriteLine("sei troppo debole e muori");
                vita = 0;
                return false;

            }
            // scappa perche non hai vita
            else if (vita <= 50 && resistenza >= 20)
            {
                Console.WriteLine("non hai abbastanza vita, devi scappare");
                Console.WriteLine("Tu: questa volta ti e andata bene mostro");
                Console.WriteLine("Demogorgone: hahaha bravo scappa!");
                resistenza = resistenza - 10;
                return true;
            }

            Console.WriteLine("combatti contro il demogorgone");
            Console.WriteLine("Demogorgone: ora morirai");
            Console.WriteLine("Tu: non ne sono sicuro");
            // ciclo combattimento
            while (vita > 0 && vitaDemo > 0)
            {

                Console.WriteLine("vuoi combattere (si) o (no)");
                string scelta = Console.ReadLine();
                if (scelta == "no")
                {
                    // scappa con resistenza 
                    if (resistenza >= 20)
                    {
                        Console.WriteLine("sei scappato!");
                        Console.WriteLine("Demogorgone: paura ehh");
                        resistenza = resistenza - 10;
                        return true;
                    }
                    // scappa ma non hai resistenza, quindi muori
                    else
                    {
                        Console.WriteLine("non hai abbastanza resistenza per scappare, muori!");
                        Console.WriteLine("Tu: questa volta ti e andata bene mostro");
                        vita = 0;
                        return false;
                    }
                }

                // attacco  con arma
                if (armi > 0)
                {
                    dannoGiocatore = 25;
                    Console.WriteLine("hai colpito il demogorgone con un'arma");
                    Console.WriteLine("Tu: prendi questo!");
                    Console.WriteLine("Demogorgone: GRAAAAAHHHHH!!");
                }
                // colpo a mani nude
                else
                {
                    dannoGiocatore = 10;
                    Console.WriteLine("hai colpito il demogorgone a mani nude");
                    Console.WriteLine("Tu: assaggia il mio sangue");
                }

                vitaDemo = vitaDemo - dannoGiocatore;
                Console.WriteLine("vita demogorgone: " + vitaDemo);

                // attacco demogorgone
                vita = vita - 10;
                resistenza = resistenza - 5;
                Console.WriteLine("il demogorgone ti ha colpito ");
                Console.WriteLine("Demogorgone: 'squittisce'");
                Console.WriteLine("Tu: non ci voleva");


                Console.WriteLine("vita: " + vita + " resistenza: " + resistenza);
            }
            //condizione vittoria contro demogorgone
            if (vitaDemo == 0)
            {
                Console.WriteLine("Hai sconfitto il demogorgone!");
                Console.WriteLine("Tu: questa volta ti e andata male mostro");
                Console.WriteLine("Demogorgone: 'ultimo ruggito'");


                return true;
            }

            return false;
        }

        // funzione eventi casella
        static void EventiCasella(string luogo, ref int vita, ref int resistenza, ref int pMappa, ref int armi, ref int cure, ref bool cavalcatura, ref bool gioco)
        {
            // evento trova cura
            if (luogo == "ospedale di Hawkins" || luogo == "casa di Hopper" || luogo == "casa dei Byers" || luogo == "Pennhurst Mental Hospital")
            {
                cure++;
                Console.WriteLine("Tu: una pozione curativa, proprio quello che mi serve!!");

            }
            // evento trova arma
            else if (luogo == "casa dei Wheeler" || luogo == "Hawkins Middle School" || luogo == "Hawkins Police Department" || luogo == "laboratorio russo")
            {
                armi++;
                Console.WriteLine("hai trovato un'arma!");
                Console.WriteLine("Tu: Perfetto, sempre bene avere un'arma");
            }
            // evento incontro Dustin
            else if (luogo == "Lover’s Lake")
            {
                Console.WriteLine("hai incontrato Dustin");
                Console.WriteLine("Dustin: ehi! Ti vedo un po' messo male.");
                Console.WriteLine("Tu: Dustin da quanto tempo!");
                if (vita < 100)
                {
                    vita = 100;
                    Console.WriteLine("Dustin ti ha curato completamente");
                    Console.WriteLine("Tu: Grazie Dustin, mi sento di nuovo al top!");
                }
                else
                {
                    Console.WriteLine("la tua vita è già al massimo, mi dipsiace");
                    Console.WriteLine("Dustin: ah ok, allora resta in guardia!");
                    Console.WriteLine("Tu: arrivederci Dustin, per ora grazie!!");
                }
            }
            // evento resistenza
            else if (luogo == "Hawkins Public Library")
            {
                Console.WriteLine("hai trovato un libro che ti da dei trucchi per avere maggiore resistenza");
                resistenza = resistenza + 20;
                Console.WriteLine("Tu: Un libro interessante... sembra aumentare la mia resistenza!");
                Console.WriteLine("Tu: wow! mi e salita di 20!! Resistenza: " + resistenza);
            }
            // evento cibo
            else if (luogo == "Bradley's Big Buy")
            {
                Console.WriteLine("Tu: mhh del cibo!!");
                vita = vita + 20;
                Console.WriteLine("hai mangiato ora hai " + vita + " di vita");

            }
            // evento trovi cavalcatura
            else if (luogo == "Palace Arcade" || luogo == "Starcourt Mall" || luogo == "Hawkins Middle School")
            {
                if (cavalcatura == false)
                {
                    Console.WriteLine("Tu: oh cavoli, una cavalcatura!");
                    Console.WriteLine("hai usato la cavalcatura, il prossimo tiro avanzerai di una casella in piu");
                    Console.WriteLine(" vuoi usarla subito? si o no");
                    string scelta = Console.ReadLine();

                    if (scelta == "si")
                    {

                        cavalcatura = true;
                    }
                    else
                    {
                        Console.WriteLine("hai lasciato la cavalcatura");
                    }
                }
            }
            // evento trovi Steve
            else if (luogo == "Radio Shack")
            {
                Console.WriteLine("hai incontrato Steve");
                Console.WriteLine("Steve: Ehi! Ti serve un po' di aiuto?");
                Console.WriteLine("Tu: Steve ti prego riforniscimi di piombo");
                if (armi < 3)
                {
                    armi = 3;
                    Console.WriteLine("Steve ti ha equipaggiato al massimo");
                    Console.WriteLine("Tu: Grazie Steve, adesso sono pronto!");
                }
                else
                {
                    Console.WriteLine("hai già troppe armi, Steve non vuole dartele");
                    Console.WriteLine("Tu: sei sempre il solito egoista, pero grazie lo stesso");
                }

            }
            // eventi demogorgone
            else if (luogo == "Forest Hills Trailer Park" || luogo == "casa di Victor Creel" || luogo == "Hawkins National Laboratory" || luogo == "Sottosopra")
            {
                Console.WriteLine("hai incontrato un demogorgone");
                Console.WriteLine("Demogorgone: GRRRRRAAAHHH! ");
                Console.WriteLine(" Tu: devo affrontarlo!");
                bool risultato = Combatti(ref vita, ref resistenza, ref armi);

                if (risultato == false)
                {
                    Console.WriteLine("hai perso");
                    Console.WriteLine("Tu: non ce l'ho fatta... il demogorgone mi ha sopraffatto");
                    gioco = false;
                }
            }
            // evento finale Vecna
            else if (luogo == "cava di Vecna")
            {
                Console.WriteLine("Vecna: sei arrivato fin qui... vediamo se sei degno");
                Console.WriteLine("######################################################");
                Console.WriteLine("##                   Vecna                          ##");
                Console.WriteLine("######################################################");
                Console.WriteLine("");
                Console.WriteLine("            .|||||||:::.         ");
                Console.WriteLine("         /|||||||||||||||\\       ");
                Console.WriteLine("       / /||@ @ @ @ @||\\ \\      ");
                Console.WriteLine("      | | ||\\  VVV  /|| | |      ");
                Console.WriteLine("      \\_\\ || `\\_M_/` || /_/      ");
                Console.WriteLine("       [||] ::::|:::: [||]       ");
                Console.WriteLine("      / /| \\/ /|\\ \\/| \\ \\      ");
                Console.WriteLine("     / / || |/ | | |/ | || \\ \\     ");
                Console.WriteLine("    / /  || | |\\ /| | ||  \\ \\    ");
                Console.WriteLine("   | |   || | |X X| | ||   | |   ");
                Console.WriteLine("   \\_\\   || | |# #| | ||   /_/   ");
                Console.WriteLine("    `--- |##| |###| |##| ---`    ");
                Console.WriteLine("        |#@#| |@@@| |#@#|        ");
                Console.WriteLine("        //||\\ |#@#| /||\\\\        ");
                Console.WriteLine("       // /| | | | | |\\ \\\\       ");
                Console.WriteLine("      // /  | | | | |  \\ \\\\      ");
                Console.WriteLine("\";");
                Console.WriteLine("");
                Console.WriteLine("######################################################");
                //condizione vittoria contro Vecna
                if (armi > 0 && vita > 70 && resistenza > 40)
                {
                    Console.WriteLine("Tu: ci sono riuscito! Ho sconfitto Vecna!");
                    Console.WriteLine("Vecna: nooooooo!");
                    Console.WriteLine("hai sconfitto Vecna!!  Hai vintoo!!!");
                }
                //condizione sconfitta contro Vecna
                else
                {

                    Console.WriteLine("Tu: non ce la faccio...");
                    Console.WriteLine("Vecna: la tua debolezza ti condanna");
                    Console.WriteLine("sei morto");
                    Console.WriteLine("Game over!!");
                    vita = 0;
                }
                Console.WriteLine("il gioco finisce qui. Grazie per averci giocato!");
                gioco = false;

            }
        }


        static void Main()
        {
            // iistruzioni di gioco
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("benvenuto nel mondo di stranger things!");
            Console.WriteLine("in questo gioco ti muoverai sulla mappa, troverai oggetti e combatterai mostri.");
            Console.WriteLine("ogni turno puoi:");
            Console.WriteLine("1 - avanzare sulla mappa");
            Console.WriteLine("2 - controllare il tuo status");
            Console.WriteLine("3 - aprire l'inventario");
            Console.WriteLine("4 - usare una pozione curativa");
            Console.WriteLine("5 - uscire dal gioco");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("attenzione ai mostri! se perdi vita puoi morire.");
            Console.WriteLine("alcuni personaggi ti daranno bonus speciali.");
            Console.WriteLine("buona fortuna!");

            string[] mappa = { "ospedale di Hawkins", "casa dei Byers", "casa dei Wheeler", "casa di Hopper", "Hawkins Middle School", "Hawkins High School", "Starcourt Mall", "Palace Arcade", "Forest Hills Trailer Park", "Hawkins Public Library", "Hawkins Police Department", "Bradley’s Big Buy", "Radio Shack", "casa di Victor Creel", "Pennhurst Mental Hospital", "Sottosopra", "Lover’s Lake", "laboratorio russo", "Hawkins National Laboratory", "cava di Vecna" };

            int vita = 100, resistenza = 100, armi = 0, cure = 0, pMappa = 0, dannoBase = 10;
            bool cavalcatura = false, gioco = true;

            // scelta personaggio
            Console.WriteLine("scegli il personaggio:");
            Console.WriteLine("1- Undici (+5 danno)");
            Console.WriteLine("2- Mike (+1 armi)");
            Console.WriteLine("3- Hopper (+50 vita)");

            int sceltaPersonaggio = Convert.ToInt32(Console.ReadLine());

            //scelta Undici
            if (sceltaPersonaggio == 1)
            {

                dannoBase = dannoBase + 5;
            }
            //scelta Mike
            else if (sceltaPersonaggio == 2)
            {

                armi++;
            }
            //scelta Hopper
            else if (sceltaPersonaggio == 3)
            {

                vita = vita + 50;
            }

            int sceltaTurno;

            do
            {
                //scelte ogni turno
                Console.WriteLine("cosa vuoi fare?");
                Console.WriteLine("1 - Avanza");
                Console.WriteLine("2 - Mostra status");
                Console.WriteLine("3 - Mostra inventario");
                Console.WriteLine("4 - Usa cura");
                Console.WriteLine("5 - Esci");

                sceltaTurno = Convert.ToInt32(Console.ReadLine());
                //scelta Avanza
                if (sceltaTurno == 1)
                {
                    int tiro = TiroDado(pMappa);
                    pMappa = pMappa + tiro;
                    if (cavalcatura == true)
                    {
                        tiro++;
                        cavalcatura = false;
                    }

                    

                    if (pMappa >= mappa.Length - 1)
                    {
                        pMappa = mappa.Length - 1;

                    }
                    Console.WriteLine("ti sei mosso di " + tiro + " caselle, sei in " + mappa[pMappa]);
                    EventiCasella(mappa[pMappa], ref vita, ref resistenza, ref pMappa, ref armi, ref cure, ref cavalcatura, ref gioco);
                    
                    //condizione per uscire dal gioco

                    if (gioco == false)
                    {
                        return;
                    }



                }


                //scelta mostra status
                else if (sceltaTurno == 2)
                {
                    Status(vita, resistenza, dannoBase, pMappa, mappa);
                }
                //scelta mostra inventario
                else if (sceltaTurno == 3)
                {
                    Inventario(armi, cure, cavalcatura);
                }
                //scelta usa cura
                else if (sceltaTurno == 4)
                {
                    if (cure > 0)
                    {
                        vita = vita + 30;
                        resistenza = resistenza + 15;
                        cure--;
                        if (sceltaPersonaggio == 3 && vita > 150)
                        {
                            vita = 150;
                        }
                        Console.WriteLine("hai usato una pozione curativa");
                        Console.WriteLine("vita: " + vita);
                        Console.WriteLine("resistenza: " + resistenza);
                    }
                    else
                    {
                        Console.WriteLine("non hai pozioni curative");
                    }
                }
                //scelta abbandona gioco
                else if (sceltaTurno == 5)
                {
                    gioco = false;
                    Console.WriteLine("hai abbandonato il gioco");
                    Console.WriteLine("Game over!!!");
                }
                else
                {
                    Console.WriteLine("scelta non valida, riprova");
                }

            }
            while (vita > 0 && gioco);
            //condizione per restare dentro il gioco

            if (vita <= 0)
            {
                Console.WriteLine("Sei morto! Game Over.");
            }
        }
    }
}