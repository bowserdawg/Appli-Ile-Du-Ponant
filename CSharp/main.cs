using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

class SitePonant
{
    struct Passagers
    {
        public string nom;
        public string prenom;
        public string codeCategorie;
        public float prix;

        public Passagers(string nm, string pn, int age, List<float> listePrix)
        {
            nom = nm;
            prenom = pn;

            if (age > 25)
            {
                codeCategorie = "adu26p";
                prix = listePrix[0];
            }
            else if (age >= 18)
            {
                codeCategorie = "jeu1825";
                prix = listePrix[1];
            }
            else if (age >= 4)
            {
                codeCategorie = "enf417";
                prix = listePrix[2];
            }
            else if (age >= 0)
            {
                codeCategorie = "bebe";
                prix = listePrix[3];
            }
            else
            {
                codeCategorie = "ancomp";
                prix = listePrix[4];
            }
        }
    }
    struct Vehicule
    {
        public string codeCategorie;

        public string nomVehicule;

        public float prix;

        public Vehicule(string cat, string nm, float px)
        {
            nomVehicule = nm;
            codeCategorie = cat;
            prix = px;
        }
    }
    struct Reservation
    {
        public string nom;
        public Traversée laTraversée;
        public string horodatage;

        public List<Passagers> listePassagers;
        public Dictionary<Vehicule, int> dicoVehiculeQuant;

        public Reservation(string nm, Traversée trav, List<Passagers> lP, Dictionary<Vehicule, int> dV)
        {
            nom = nm;
            laTraversée = trav;
            listePassagers = lP;
            dicoVehiculeQuant = dV;
            horodatage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
    struct Traversée
    {
        public string idLiaison;
        public string date;
        public string heure;
        // Les deux données suivants ne sont pas utiles pour le JSON mais il faut prevenir l'utilisateur
        public string bateau;
        public bool vehiculeAllowed;

        public Traversée(string idL, string d, string h, string b, bool NA)
        {
            idLiaison = idL;
            date = d;
            heure = h;
            bateau = b;
            vehiculeAllowed = NA;
        }
    }
   
    static void printTraversee(Traversée unTrav)
    {
        Console.WriteLine("\tHoraire départ : " + unTrav.heure);
        Console.WriteLine("\tNom du bateau : " + unTrav.bateau);
        Console.WriteLine("\tVehicule autorisée ? : " + unTrav.vehiculeAllowed + "\n");
    }

    static void printVehicule(Vehicule vehicule)
    {
        Console.WriteLine("\t" + vehicule.nomVehicule);
        Console.WriteLine("\tprix : " + vehicule.prix + "€");
    }

    static void printPass(Passagers pass)
    {
        Console.WriteLine("\tNom : " + pass.nom);
        Console.WriteLine("\tPrenom : " + pass.prenom);
        Console.WriteLine("\tcodeCategorie age : " + pass.codeCategorie);
        Console.WriteLine("\tTarrifs : " + pass.prix + "€");
    }
    
    static void printReservation(Reservation reservation)
    {
        Console.WriteLine("\t" + reservation.nom);
        printTraversee(reservation.laTraversée);
        Console.WriteLine("\t" + reservation.horodatage);
        printListPass(reservation.listePassagers);
        foreach (Vehicule vehicule in reservation.dicoVehiculeQuant.Keys)
        {
            // Ecrit le nom du vehicule et son prix
            printVehicule(vehicule);
            Console.WriteLine("Quantité : " + reservation.dicoVehiculeQuant[vehicule]);
        }
    }

    static void demander(ref bool choix, string texte)
    {
        // Pour demander
        string ouiNon = "";
        while (ouiNon.ToLower() != "oui" && ouiNon.ToLower() != "non")
        {
            Console.WriteLine(texte);
            ouiNon = Console.ReadLine();
        }
        if (ouiNon.ToLower() == "oui")
        {
            choix = true;
        }
        else if (ouiNon.ToLower() == "non")
        {
            choix = false;
        }
    }

    static void demander3(ref bool enleverPassager, ref bool continuerAjoutPassager, string[] listeChoix, string texte)
    {
        if (listeChoix.Length != 3)
        {
            Console.WriteLine("ERREUR fonction demander3 : Il faut 3 choix en paramétres");
        }
        else
        {
            // Pour demander
            string reponse = "";
            while (reponse.ToLower() != listeChoix[0] && reponse.ToLower() != listeChoix[1] && reponse.ToLower() != listeChoix[2])
            {
                Console.WriteLine(texte);
                reponse = Console.ReadLine();
            } // Non choisi
            
            if (reponse.ToLower() == listeChoix[0])
            {
                enleverPassager = true;
                continuerAjoutPassager = true; 
            } // Choix 1 : le premier choix (Enlever), on continue a ajouter quand meme
            
            else if (reponse.ToLower() == listeChoix[1])
            {
                enleverPassager = false;
                continuerAjoutPassager = true;
            } // Choix 2 : le deuxieme choix (Rajouter), on continue normallement si tous c'est bien passé
            
            else if (reponse.ToLower() == listeChoix[2])
            {
                enleverPassager = false;
                continuerAjoutPassager = false;

            } // Choix 3 : on arrete de ajouter
            
        }
    }

    static int getInt(int max, int min)
    {
        int nombre = 0;

        bool choixValide = false;
        while (!choixValide)
        {
            choixValide = int.TryParse(Console.ReadLine(), out nombre);

            // Si le jour n'est pas dans le range
            if (nombre > max || nombre <= min)
            {
                choixValide = false;
                Console.WriteLine("Nombre incorrecte, veuillez ressayer:");
            }
        }

        return nombre;   
    }

    /*static int getInt(int max, int min)
    {
        int nombre = 0;

        bool choixValide = false;
        while (!choixValide)
        {
            choixValide = int.TryParse(Console.ReadLine(), out nombre);

            // Si le jour n'est pas dans le range
            if (nombre > max || nombre <= min)
            {
                choixValide = false;
                Console.WriteLine("Nombre incorrecte, veuillez ressayer:");
            }
        }

        return nombre;
    }*/

    static float ConvertToFloat(string chaine)
    {
        float flottant = 0.00f;

        if (float.TryParse(chaine, out flottant))
        {
            Console.WriteLine("Loading...");
        }
        else
        {
            Console.WriteLine("Erreur! La BDD à des valeurs incorrectes");
        }

        Console.WriteLine(flottant);

        return flottant;
    }


    static void afficherEtapesPile(Stack<string> pile)
    {
        // Affiche à l'utilisateur l'étape suivante et aussi les étapes précédente.
        Stack<string> tempStack = new Stack<string>(new Stack<string>(pile));
        int nbEtape = tempStack.Count;

        Console.Write("Etapes suivantes: : ");
        Console.WriteLine("\t" + nbEtape + " : " + tempStack.Pop() + "\nEtapes précédente:");
        nbEtape--;
        // Les étapes précédente
        while(tempStack.Count > 0)
        {
            Console.WriteLine("\t\t\t" + nbEtape + " : " + tempStack.Pop());
            nbEtape--;
        }
    }

    static void empilerEtapeDesiree(Stack<string> pile, int nbDesire, ref bool etapeDejaTermine, Dictionary<string, string> etapeSuivante)
    {
        //Stack<string> tempStack = new Stack<string>(new Stack<string>(pile));

        //while(tempStack.Count > nbDesire)
        //{
        //    tempStack.Pop();
        //}

        //pile.Push(tempStack.Peek());

        // Empile une etape precédente si l'utilisateur veut cela.
        string Etape = "Etape " + nbDesire.ToString();

        pile.Push(etapeSuivante[Etape]);

        etapeDejaTermine = true;
    }

    static void checkEtapeSuivante(ref bool etapeDejaTermine, Stack<string> pileEtape, Dictionary<string, string> etapeSuivante, string texte)
    {
        // Si l'etape etait en fait refait par l'utilisateur, on enleve la pile qu'on a rajouté temporairement
        if (etapeDejaTermine)
        {
            etapeDejaTermine = false;
            pileEtape.Pop();
        }
        // Sinon, on continue
        else
        {
            pileEtape.Push(etapeSuivante[texte]);   
        }
    }

  /*  static void sauvegarder(Stack<string> pileEtape)
    {
        bool allerEnArriere = true;

        demander(ref allerEnArriere, 
                 "Voulez vous continuer? [Tapez oui pour continuer, non pour aller en arriere]");
        
        if (!allerEnArriere)
        {
            pileEtape.Pop();
        }
    } */

// Le nom : 1 fonctions
    static string choisirNom()
    {
        // Cette fonction est pour assigner un nom à la reservation;
        Console.Clear();
        Console.WriteLine("Choisissez un nom pour votre reservation");
        string nom = Console.ReadLine();
        return nom;
    }

// La traversée : 8 fonctions
    // Les Liaisons : 2 fonctions
    static void chargeLiaison(Dictionary<string,string> dicoLiaisons)
    {
        string liaison;
        string[] temp;
        char[] separateur = new char[] {';'};

        // Liste pour tous les Liaison
        if (File.Exists("Liaisons.csv"))
        {
            //Ouverture en Lecture
            //En 2 temps
            //1-on crée un flux qui permet de lire des fichiers…
            FileStream fs = new FileStream("Liaisons.csv", 
                FileMode.Open, FileAccess.Read
            );
            //2-Ce flux sera de type texte
            StreamReader fluxLiaisons = new StreamReader(fs);
            while (!fluxLiaisons.EndOfStream)
            {
                // La liaison en cours de lecture
                liaison = fluxLiaisons.ReadLine();
                // Eclater
                temp = liaison.Split(separateur);
                // Ajouter dans le dico
                dicoLiaisons.Add(temp[0], temp[1]);
            }

            fluxLiaisons.Close();
        }
    }

    static string afficherEtDemanderLiaisons()
    {
        Dictionary<string,string> dicoLiaisons = new Dictionary<string,string>();

        chargeLiaison(dicoLiaisons);

        // Affichage
        Console.Clear();
        Console.WriteLine("Choissisez une liaison:\n");
        Console.WriteLine("Pour choisir, tapez le numero correspondant à votre ile.\n" + 
                          "Tapez ''Quitter'' pour revenir en arriere\n"
                        );
        foreach(string liaison in dicoLiaisons.Keys)
        {
            Console.WriteLine(liaison + " : " + dicoLiaisons[liaison]);
        }

        // L'utilisateur choisi la liaison
        string idLiaison = Console.ReadLine();
        // Verifie si le id donné par le client est correcte
        while (!dicoLiaisons.ContainsKey(idLiaison))
        {
            Console.WriteLine("Numero de Liaison incorrecte, veuillez choisir un numero valide");
            idLiaison = Console.ReadLine();
        }

        return idLiaison;
    }

    // Les traversées d'une liaison: 5 fonctions

    static void printTravJour(Dictionary<int, List<Traversée>> dicoTrav, int jourChoisi)
    {
        int numeroDeLaTrav = 1;
        foreach(Traversée trav in dicoTrav[jourChoisi])
        {
            Console.WriteLine("Traversée Numero " + numeroDeLaTrav + " :");
            printTraversee(trav);
            numeroDeLaTrav++;
        }
    }

    static void chargeTraverseeMois(Dictionary<int,List<Traversée>> dicoTrav, string idLiaison, int mois)
    {
        const int annee = 2025;
        // Efface le contenu du dico pour etre sur
        dicoTrav.Clear();
        // Trouver le nom du fichier
        string[] listeMois =
        {
            "Janvier", 
            "Fevrier",
            "Mars",
            "Avril",
            "Mai",
            "Juin",
            "Juillet",
            "Aout",
            "Septembre",
            "Octobre",
            "Novembre",
            "Decembre"
        };
        string dossierFichier = "./horaires";
        dossierFichier = dossierFichier + listeMois[mois - 1]; // - 1 car indice
        dossierFichier = dossierFichier + "/" + idLiaison + ".csv";
        /////////////////////////////////////

        // Liste pour tous les Liaison
        if (File.Exists(dossierFichier))
        {
            string horaires;
            string bateau;
            string[] tempHoraires;
            string[] tempBateau;
            char[] separateur = new char[] {';'};
            int jour = 1;
            List<Traversée> listeTravCourrant;
            bool vehiculeAllowed = true;

            //Ouverture en Lecture
            //En 2 temps
            //1-on crée un flux qui permet de lire des fichiers…
            FileStream fs = new FileStream(dossierFichier, 
                FileMode.Open, FileAccess.Read
            );
            //2-Ce flux sera de type texte
            StreamReader fluxTraversee = new StreamReader(fs);

            while (!fluxTraversee.EndOfStream)
            {
                // Les horaires et les bateaus en cours de lecture
                horaires = fluxTraversee.ReadLine();
                bateau = fluxTraversee.ReadLine();

                // Eclater les valeurs
                tempHoraires = horaires.Split(separateur);
                tempBateau = bateau.Split(separateur);

                // Déclarer la liste à chaque fois pour le mettre dans le dico
                listeTravCourrant = new List<Traversée>();
                // Ajouter dans le dico et incrementer le jour
                for (int i = 0; i < tempHoraires.Length; i++)
                {
                    // Si les vehicules ne sont pas autorisées 
                    if (tempHoraires[i][tempHoraires[i].Length - 1] == 'n')
                    {
                        vehiculeAllowed = false;
                    }
                    // Pour la date
                    string date = Convert.ToString(annee) + "-" + Convert.ToString(mois) + "-" + Convert.ToString(jour);
                    // Ajouter la traversée
                    listeTravCourrant.Add(new Traversée(idLiaison, date, tempHoraires[i], tempBateau[i], vehiculeAllowed));
                    vehiculeAllowed = true;
                }
                dicoTrav.Add(jour, listeTravCourrant);
                jour++; 
            }

            fluxTraversee.Close();
        }
        else
        {
            Console.WriteLine("Pas de traversee valable pour ce mois");
        }
    }

    static void printTraverseeDuMois(Dictionary<int, List<Traversée>> dicoTrav)
    {
        Console.Clear();
        Console.WriteLine("Jour | nombre de traversée disponible(s)\n");
        foreach(int jour in dicoTrav.Keys)
        {
            Console.Write(jour);
            //int i = 0;
            //if (jour >= 10) {i = 1;} // Si le jour est plus grand ou 
            //                         // egale à 10, on affiche 7 espaces au lieu de 8
            //                         // pour bien alligner
            //for (i = i; i < 8; i++) {Console.Write(" ");}
            //Console.Write(dicoTrav[jour].Count + " traversée(s) disponible(s)\n");
            Console.Write("\t" + dicoTrav[jour].Count + " traversée(s) disponible(s)\n");
        }
    }

    static List<Traversée> demandeEtAfficheListeTraverseeJournee(Dictionary<int, List<Traversée>> dicoTrav, 
                                                            int jourMax,
                                                            ref bool retourDesire,
                                                            string idLiaison,
                                                            int mois)
    {
        // Aller (premier élement) et retour

        List<Traversée> AllerRetour = new List<Traversée>();
        
        //////////////////////////
        // Le jour (jourChoisi) //
        //////////////////////////
        // Afficher les traversées du mois choisi
        printTraverseeDuMois(dicoTrav);
        Console.WriteLine("Quelle jour voulez vous inspecter?:");
        int jourChoisi = getInt(jourMax, 0);

        //////////////////////////////////////////////////
        // Affichage de tous les horaires de la journée //
        //////////////////////////////////////////////////
        Console.Clear();
        Console.WriteLine("Informations sur tous les traversés de la liaison numero " + 
                          dicoTrav[jourChoisi][0].idLiaison + " pour le jour " + jourChoisi + "\n"
                         );

        ///////////////////////////////
        // La traversée (travChoisi) //
        ///////////////////////////////
        printTravJour(dicoTrav, jourChoisi);
        Console.WriteLine("Quelles traversées choisissez vous?");
        int travChoisi = getInt(dicoTrav[jourChoisi].Count, 0);
        ////////////////////////////////////////////
 
        // Ajouter l'aller à la liste
        AllerRetour.Add(dicoTrav[jourChoisi][travChoisi - 1]);
        
        ///////////////////////////////
        //    Demander un retour     //
        ///////////////////////////////
        
        // Voir si un retour est disponible
        if (travChoisi < dicoTrav[jourChoisi].Count)
        {
            demander(ref retourDesire, "Voulez vous resever un retour? [Tapez oui/non]");

            int numRetourChoisi;

            // On declare le retour (obligé)
            if (retourDesire)
            {
                // Choisir le bon idLiaison
                string idLiaisonRetour = "";
                // Un peu de math pour load la traversée correcte
                if (int.Parse(idLiaison) % 2 == 0)
                {
                    int temp = int.Parse(idLiaison) - 1;
                    idLiaisonRetour = temp.ToString();
                }
                else
                {
                    int temp = int.Parse(idLiaison) + 1;
                    idLiaisonRetour = temp.ToString();
                }

                Dictionary<int, List<Traversée>> listeRetour = new Dictionary<int, List<Traversée>>();
                chargeTraverseeMois(listeRetour, idLiaisonRetour, mois);
                Console.Clear();
                printTravJour(listeRetour, jourChoisi);
                Console.WriteLine("Quelles traversées voulez vous choisir pour votre retour ?");

                numRetourChoisi = getInt(listeRetour[jourChoisi].Count, 0);
                AllerRetour.Add(listeRetour[jourChoisi][numRetourChoisi - 1]);
            }
        }

        return AllerRetour;
    }

    static List<Traversée> afficherEtDemanderTraversee(string idLiaison, ref bool retourChoisi)
    {
        ////////////////////////////
        // Le mois et le jour max //
        ////////////////////////////
        int jourMax = 30;
        int mois = 11; //Implementation stupide car seulement novembre dans la SAE, 
                       // mais pourait etre utile si on veut mettre d'autres mois avec une implementation
        if (mois > 7 && mois % 2 == 0)
        {
            jourMax = 31;
        }
        else if (mois == 2)
        {
            jourMax = 28; // On pourrait utiliser avec DateTime.IsLeapYear(Int32) 
        }
        else if (mois < 7 && mois % 2 == 1)
        {
            jourMax = 31;
        }

        //////////////////////////////////////////
        // Charger les traversée de la liaison  //
        //////////////////////////////////////////
        Dictionary<int, List<Traversée>> dicoTrav = new Dictionary<int, List<Traversée>>();
        chargeTraverseeMois(dicoTrav, idLiaison, mois);

        /////////////////////////////////////////////////
        // Demande une traversee pour le jour demander //
        /////////////////////////////////////////////////
        List<Traversée> travFinal;
        travFinal = demandeEtAfficheListeTraverseeJournee(dicoTrav, jourMax, ref retourChoisi, idLiaison, mois);
        /////////////////////////////////////////////////////////////////////////

        return travFinal;
    }

    // Un peu comme un sous-main

    static List<Traversée> choisirLiaisonEtTraversée(ref bool retourChoisi)
    {
        // Idliaison
        string idLiaison;
        idLiaison = afficherEtDemanderLiaisons();
        // Traversee
        List<Traversée> laTraversee = new List<Traversée>();
        laTraversee = afficherEtDemanderTraversee(idLiaison, ref retourChoisi);

        return laTraversee;
    }

// Passager : 5 fonctions

    static void chargePrixPassager(List<float> tarrifs, string idLiaison)
    {
        string prix;
        string[] temp;
        char[] separateur = new char[] {';'};
        int numeroLiaison = 1;

        // Liste pour tous les prix
        if (File.Exists("TarifsLiaisons.csv"))
        {
            //Ouverture en Lecture
            //En 2 temps
            //1-on crée un flux qui permet de lire des fichiers…
            FileStream fs = new FileStream("TarifsLiaisons.csv", 
                FileMode.Open, FileAccess.Read
            );
            //2-Ce flux sera de type texte
            StreamReader fluxPrix = new StreamReader(fs);
            // Parcourir tous les liaisons
            while (!fluxPrix.EndOfStream)
            {
                // La prix en cours de lecture
                prix = fluxPrix.ReadLine();

                // Eclater
                temp = prix.Split(separateur);

                if (numeroLiaison == int.Parse(idLiaison))
                {
                    // Ajouter dans la liste
                    for (int i = 0; i < temp.Length; i++)
                    {
                        tarrifs.Add(ConvertToFloat(temp[i]));
                    }
                }

                Console.WriteLine("Tache fini avec succes");

                numeroLiaison++;
            }

            fluxPrix.Close();
        }
    }

    static void printListPass(List<Passagers> listePassager)
    {
        int n = 1;
        foreach (Passagers pass in listePassager)
        {
            Console.WriteLine("Passager numero " + n);
            printPass(pass);
            Console.Write("\n");
            n++;
        }
    }

    static void removePassager(List<Passagers> listPassager)
    {   
        bool continuerRemove = true;

        while (continuerRemove && listPassager.Count > 0)
        {
            Console.Clear();

            // Affichage de la liste
            Console.WriteLine("Voici la liste:");
            printListPass(listPassager);
            
            // Demander quelle passagers ils veulent enlever
            Console.WriteLine("Veuillez choisir le numero du passager que vous voulez enlevez");
            int nbChoisiRemove = getInt(listPassager.Count, 0);

            // Enlever
            listPassager.RemoveAt(nbChoisiRemove - 1);

            // Si l'utilisateur veut continuer
            if (listPassager.Count > 0)
            {
                demander(ref continuerRemove, "Voulez vous continuez à enlever des passagers? [Tapez oui/non]");
            }
        }

    }

    static Passagers getPassager(Passagers passagerCourant, List<float> tarrifs, bool ancomp)
    {
        string nom, prenom;
        int age = -1;

        string duPassager = " du passager"; // Pour changer le texte, si c'est un passager ou un animal
        if (ancomp)
        {
            duPassager = " de l'animal";
        }

        Console.WriteLine("Prenom" + duPassager + " : ");
        prenom = Console.ReadLine();
        Console.WriteLine("Nom" + duPassager + " : ");
        nom = Console.ReadLine();

        if (!ancomp)
        {
            Console.WriteLine("Age" + duPassager + " : ");
            age = getInt(150, -1);
        }
        passagerCourant = new Passagers(nom, prenom, age, tarrifs);

        return passagerCourant;
    }

    static List<Passagers> listerLesPassagers(string idLiaison)
    {
        Console.Clear();
        // Liste
        List<Passagers> listePassagers = new List<Passagers>();
        // Bool continuer
        bool continuerAjoutPassager = true;
        // Bool si l'utilisateur veut enlever un passager
        bool enleverPassager = true;
        // Le passager
        Passagers passagerCourant = new Passagers();
        int passagerNumero = 1;
        // Prix par passager (depend de l'age)
        List<float> tarrifs = new List<float>();
        chargePrixPassager(tarrifs, idLiaison);
        // Pour question à 3 choix
        string[] choix_3 = {"enl", "raj", "arr"};

        while (continuerAjoutPassager)
        {
            Console.Clear();
            Console.WriteLine("Passager numero " + (listePassagers.Count + 1));
            listePassagers.Add(getPassager(passagerCourant, tarrifs, false));

            Console.WriteLine("Voici tous les passagers : ");
            printListPass(listePassagers);

            // Les deux bool pour nos 3 choix (ajouté ici pour rendre plus lisible)
            enleverPassager = false;
            continuerAjoutPassager = true;
            demander3(ref enleverPassager, ref continuerAjoutPassager, choix_3, "Voulez vous ENLEVEZ des passagers, continuer à ajouter des passagers ou arreter ? [Tapez ENL/RAJ/ARR respectivement]");

        
            // Si l'utilisateur veut enlever un passager
            if (enleverPassager)
            {
                removePassager(listePassagers);
                // On force l'utilisateur à avoir au moins 1 passager
                if (listePassagers.Count > 0)
                {
                    demander(ref continuerAjoutPassager, "Voulez vous continuer à rajouter des passagers? [Tapez  Oui/Non]");
                }   
            } 

            passagerNumero++;
        }

        bool animalCo = false;
        demander(ref animalCo, "Avez vous un ou plusieurs animaux de compagnie que vous voulez emmener avec vous? [Tapez oui/non]");
        while (animalCo)
        {
            Console.Clear();
            Console.WriteLine("Animal de comp. ");
            listePassagers.Add(getPassager(passagerCourant, tarrifs, true));

            Console.WriteLine("Voici tous les passagers et animaux de compagnies : ");
            printListPass(listePassagers);

            // On utilise le meme bool
            enleverPassager = false;
            demander3(ref enleverPassager, ref animalCo, choix_3, "Voulez vous ENLEVEZ des passagers, continuer à ajouter des passagers/ancomp ou arreter ? [Tapez ENL/RAJ/ARR respectivement]");

            if (enleverPassager)
            {
                // c'est drole car il paut y'avoir une reservation que avec un animal
                removePassager(listePassagers);
                // Pour ne pas avoir une reservation avec 0 personne
                if (listePassagers.Count > 0)
                {
                    demander(ref animalCo, "Voulez vous continuer à rajouter des animaux? [Tapez  Oui/Non]");
                }   
            } 

            passagerNumero++;
        }

        return listePassagers;
    }

// Les vehicules : 6 fonctions

    static void chargeListePrixVehicules(Dictionary<int, Vehicule> dicoVehiculeQuant, Dictionary<string,string> dicoCodeNomVehicule, int idLiaison)
    {
        string prix;
        string[] temp;
        char[] separateur = new char[] {';'};
        int numero = 1;
        Vehicule uVehicule;

        // Liste pour tous les prix
        if (File.Exists("PrixVehiculesLiaisons.csv"))
        {
            //Ouverture en Lecture
            //En 2 temps
            //1-on crée un flux qui permet de lire des fichiers…
            FileStream fs = new FileStream("PrixVehiculesLiaisons.csv", 
                FileMode.Open, FileAccess.Read
            );
            //2-Ce flux sera de type texte
            StreamReader fluxPrix = new StreamReader(fs);
            while (!fluxPrix.EndOfStream)
            {
                // La prix en cours de lecture
                prix = fluxPrix.ReadLine();

                // Eclater
                temp = prix.Split(separateur);

                // Code, Nom et Prix de la Liaison specifique
                uVehicule = new Vehicule(
                                            temp[0], 
                                            dicoCodeNomVehicule[temp[0]], 
                                            ConvertToFloat(temp[idLiaison])
                                        );

                // Ajouter dans le dico
                dicoVehiculeQuant.Add(numero, uVehicule);
                numero++;
            }

            fluxPrix.Close();
        }
    }

    static void chargeNomVehicules(Dictionary<string,string> dicoCodeNomVehicule)
    {
        string vehicule;
        string[] temp;
        char[] separateur = new char[] {';'};

        // Liste pour tous les prix
        if (File.Exists("Vehicules.csv"))
        {
            //Ouverture en Lecture
            //En 2 temps
            //1-on crée un flux qui permet de lire des fichiers…
            FileStream fs = new FileStream("Vehicules.csv", 
                FileMode.Open, FileAccess.Read
            );
            //2-Ce flux sera de type texte
            StreamReader fluxVehicules = new StreamReader(fs);
            while (!fluxVehicules.EndOfStream)
            {
                // Le vehicule en cours de lecture
                vehicule = fluxVehicules.ReadLine();
                // Eclater
                temp = vehicule.Split(separateur);
                // Ajouter dans le dico [type en premier, nom aprés]
                dicoCodeNomVehicule.Add(temp[1], temp[0]);
            }

            fluxVehicules.Close();
        }
    }

    static void printListeVehicule(Dictionary<int, Vehicule> dicoVehiculeQuant)
    {
        foreach (int numero in dicoVehiculeQuant.Keys)
        {
            // Ecrit le nom du vehicule et son prix
            Console.WriteLine(numero + " : ");
            printVehicule(dicoVehiculeQuant[numero]);
        }
    }

    static void printVehChoisi(Dictionary<Vehicule, int> dico)
    {
        foreach (Vehicule vehicule in dico.Keys)
        {
            printVehicule(vehicule);
            Console.WriteLine("\tQuantité : " + dico[vehicule] + "\n");
        }
    }

    static void choisirQuantVehicule(Dictionary<Vehicule, int> dicoVeh, Dictionary<int, Vehicule> dicoVehiculePossible)
    {
        Console.WriteLine("Choisir un véhicule : ");
        int numeroChoix = getInt(dicoVehiculePossible.Count, 0);

        Console.WriteLine("La quantité de vehicule que vous voulez :");
        int quantite = getInt(100, 0);

        // Pour voire si le dico contiens une clé deja choisi (l'utilisateur re-choisi)
        // dicoVehiculePossible[numeroChoix] = le vehicule choisi par l'utilisateur
        if (!dicoVeh.ContainsKey(dicoVehiculePossible[numeroChoix]))
        {
            dicoVeh.Add(dicoVehiculePossible[numeroChoix], quantite);
        }
        else
        {
            dicoVeh[dicoVehiculePossible[numeroChoix]] = quantite;
        }
    }

    static Dictionary<Vehicule, int> listerLesVehicules(string idLiaison)
    {
        Console.Clear();

        // A renvoyer
        Dictionary<Vehicule, int> dicoVehQuant = new Dictionary<Vehicule, int>();
        
        // Bool continuer
        bool continuerAjoutVehicule = true;

        // Déclarations et  Chargement
        // Le nom et code des vehicules
        Dictionary<string,string> dicoCodeNomVehicule = new Dictionary<string,string>();
        chargeNomVehicules(dicoCodeNomVehicule);
        // Construction des vehicules avec leur prix
        Dictionary<int, Vehicule> dicoVehiculePrix = new Dictionary<int, Vehicule>();
        chargeListePrixVehicules(dicoVehiculePrix, dicoCodeNomVehicule, int.Parse(idLiaison));

        while (continuerAjoutVehicule)
        {
            Console.Clear();
            printListeVehicule(dicoVehiculePrix);
            choisirQuantVehicule(dicoVehQuant, dicoVehiculePrix);

            Console.WriteLine("Voici tous les vehicules que vous avez choisi: ");
            printVehChoisi(dicoVehQuant);

            demander(ref continuerAjoutVehicule, "Voulez vous continuer a rajouter des vehicules ? [Tapez oui/non]");
        }

        return dicoVehQuant;
    }

    /////////////////////////////////////////////////////////

    static List<Reservation> Reserver()
    {
        // Elements dont on a besoin de savoir pour une reservation
        string nom = "";
        List<Traversée> traversée = new List<Traversée>{ }; // 1 ou 2 élements
        List<Passagers> listePassagers = new List<Passagers>{ }; // 1 ou n élements
        Dictionary<Vehicule, int> dicoVehicule = new Dictionary<Vehicule, int>{ }; // 0 ou n élements
        // Horodatage assigné à la construction

        // Si un retour est choisi:
        bool retourDesire = false;

        /// La pile contenat les etapes suivantes ///
        Dictionary<string, string> etapeSuivante = new Dictionary<string, string>   
        {                                                             
            {"Etape 1", "Nom"},                     
            {"Etape 2", "Traversée (date et heure)"}, 
            {"Etape 3", "Infos sur les passagers"},    
            {"Etape 4", "Infos sur les vehicule"},     
            {"Etape 5", "Confirmation : fin de la réservation"}                  
        };                                         
        Stack<string> pileEtape;                  
        pileEtape = new Stack<string>();           
        pileEtape.Push(etapeSuivante["Etape 1"]);            
        /////////////////////////////////////////////

        // Boucle reservation 
        bool reservationFini = false;
        bool etapeDejaTermine = false;
        while (!reservationFini)
        {
            // Afficher les étapes à faire ou refaire
            if (pileEtape.Count > 1)
            {
                Console.Clear();

                afficherEtapesPile(pileEtape);

                Console.Write("Quelle étape voulez vous (re)faire?");
                int etapeDesire = getInt(pileEtape.Count, 0);
                if (etapeDesire < pileEtape.Count) // Si l'utilisateur désire refaire une étape
                {
                    empilerEtapeDesiree(pileEtape, etapeDesire, ref etapeDejaTermine, etapeSuivante);   
                }
            }

            switch (pileEtape.Peek())
            {
                // Etape 1
                case "Nom":
                    nom = choisirNom();
                    checkEtapeSuivante(ref etapeDejaTermine, pileEtape, etapeSuivante, "Etape 2");
                    break;

                // Etape 2
                case "Traversée (date et heure)":
                    traversée = choisirLiaisonEtTraversée(ref retourDesire);
                    checkEtapeSuivante(ref etapeDejaTermine, pileEtape, etapeSuivante, "Etape 3");
                    break;

                // Etape 3
                case "Infos sur les passagers":
                    listePassagers = listerLesPassagers(traversée[0].idLiaison);
                    checkEtapeSuivante(ref etapeDejaTermine, pileEtape, etapeSuivante, "Etape 4");
                    break;

                // Etape 4
                case "Infos sur les vehicule":
                    if (traversée[0].vehiculeAllowed)
                    {
                        bool choix = false;
                        demander(ref choix, "Voulez vous ajouter des vehicules (Taper \"Non\" va reinitialisé vos choix précedents) [Tapez Oui/Non]");
                        if (choix)
                        {
                            dicoVehicule = listerLesVehicules(traversée[0].idLiaison); // idLiason pour le prix
                        }
                        else
                        {
                            dicoVehicule.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Info : Les vehicules ne sont pas autorisés dans votre traversée choisi [Tapez n'importe quoi pour continuer normallement]");
                        Console.ReadLine();
                    }
                    checkEtapeSuivante(ref etapeDejaTermine, pileEtape, etapeSuivante, "Etape 5");
                    break;

                // Etape 5
                case "Confirmation : fin de la réservation":
                    reservationFini = true;
                    break;
            }
        
        }

        // Creez la liste de structure et le renvoyez (liste car aller retour)
        List<Reservation> listeReservation = new List<Reservation>();

        Reservation reservationAller = new Reservation(nom, traversée[0], listePassagers, dicoVehicule);
        listeReservation.Add(reservationAller);

        Reservation reservationRetour;
        if (retourDesire)
        {
            reservationRetour = new Reservation(nom, traversée[1], listePassagers, dicoVehicule);
            listeReservation.Add(reservationRetour);
        }

        return listeReservation;
    }

    static void CreerJson(List<Reservation> reservations, string nomFichier)
    {
        int nbREs = reservations.Count;
        int nbPers = reservations[0].listePassagers.Count;
        int nbVeh = reservations[0].dicoVehiculeQuant.Count;

        string cheminNomFichier = "./JSON/" + nomFichier + ".json";

        // Ouverture/Creation en écriture 
        FileStream fs = new FileStream(cheminNomFichier,
        FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs);

        writer.WriteLine("[");

        for (int y = 0; y < nbREs; y++)
        {
            // Entete

            writer.WriteLine("  {");

            writer.WriteLine("    \"reservation\": {");

            writer.WriteLine("      \"nom\": \"" + reservations[y].nom + "\",");
            writer.WriteLine("      \"idLiaison\": " + reservations[y].laTraversée.idLiaison+ ",");
            writer.WriteLine("      \"date\": \"" + reservations[y].laTraversée.date + "\",");
            writer.WriteLine("      \"heure\": \"" + reservations[y].laTraversée.heure + "\",");
            writer.WriteLine("      \"horodatage\": \"" + reservations[y].horodatage + "\"");

            writer.WriteLine("    },");


            // Les passagers

            writer.WriteLine("    \"passagers\" : [");

            for (int i = 0; i < nbPers; i++)
            {
                writer.WriteLine("      {");
                writer.WriteLine("        \"nom\": \"" + reservations[y].listePassagers[i].nom + "\",");
                writer.WriteLine("        \"prenom\": \"" + reservations[y].listePassagers[i].prenom + "\",");
                writer.WriteLine("        \"codeCategorie\": \"" + reservations[y].listePassagers[i].codeCategorie + "\"");

                if (i < nbPers - 1)
                {
                    writer.WriteLine("      },");
                }
                else
                {
                    writer.WriteLine("      }");
                }
                
            }
                
            writer.WriteLine("    ],");

            // Les vehicules

            // Seulement pour savoir si il faut mettre une virgule;
            int numeroVeh = 0;

            writer.WriteLine("    \"vehicules\": [");

            foreach (Vehicule vehi in reservations[y].dicoVehiculeQuant.Keys)
            {
                writer.WriteLine("      {");
                writer.WriteLine("        \"codeCategorie\": \"" + vehi.codeCategorie + "\",");
                writer.WriteLine("        \"quantite\": " + reservations[y].dicoVehiculeQuant[vehi]);

                if (numeroVeh < nbVeh - 1)
                {
                    writer.WriteLine("      },");
                }
                else
                {
                    writer.WriteLine("      }");
                }
                numeroVeh++;

            }

            // Fin Vehicules 
            writer.WriteLine("    ]");

            // Fin Reservation numero y
            if (y == 1 || nbREs == 1)
            {
                writer.WriteLine("  }");
            }
            else
            {
                writer.WriteLine("  },");
            }
            
        }

        writer.WriteLine("]");

        // Ferme le fichier
        writer.Close();

    }


    static void Main()
    {
        List<Reservation>  reservation = new List<Reservation>();
        reservation = Reserver();
        CreerJson(reservation, reservation[0].nom);
    }
}