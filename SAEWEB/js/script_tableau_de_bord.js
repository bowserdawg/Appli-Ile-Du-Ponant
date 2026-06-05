getJsonLiaison = async () =>
{
    try{
        const response = await fetch(`https://can.iutrs.unistra.fr/api/liaison/all`);
        //Vérifier si la réponse est correcte
        if(!response.ok) {
            throw new Error('Erreur de réseau !');
        }

        //Sinon Retourner le résultat au format JSON
        const data = await response.json();

        return data
    } 
    catch (error) // Gérer les erreurs
    { 
        console.error('Une erreur est survenue :', error);
    }
}

const getJsonLiaisonDate = async (liaison, jour) =>
{
    try{
        const response = await fetch(`https://can.iutrs.unistra.fr/api/liaison/${liaison}/remplissage/2025-11-${jour}`);
        //Vérifier si la réponse est correcte
        if(!response.ok) {
            throw new Error('Erreur de réseau !');
        }

        //Sinon Retourner le résultat au format JSON
        const data = await response.json();

        return data
    } 
    catch (error) // Gérer les erreurs
    { 
        console.error('Une erreur est survenue :', error);
    }
}

remplirLiaison = async () =>
{
    // Mettre tous les liaisons
    containerLiaion = document.getElementById("liaison");

    const lesLiaisons = await getJsonLiaison();

    for (const liaison in lesLiaisons)
    {
        containerLiaion.innerHTML += `<option value="${lesLiaisons[liaison].id}">${lesLiaisons[liaison].nom} </option>`;
    }
}

// Executer des que l'utilisateur rentre dans la page
remplirLiaison();

remplirTrav = async () =>
{   
    // La date choisi par l'utilisateur 
    const date = new Date(document.getElementById("date").value);

    if (date.getFullYear() != 2025 || date.getMonth != 10)
    {
        //alert("Veuillez choisir une date comprise en Novembre 2025");
    }

    // La liaison choisi par l'utilisateur
    const liaison = document.getElementById("liaison").value;

    const lesTrav = await getJsonLiaisonDate(liaison, date.getDate());

    // Option de traversée à une certaine heure
    travJournee = document.getElementById("traversee");
    // Init (on ne peutr pas juste faire = ``)
    travJournee.innerHTML = ``;

    for (const trav in lesTrav)
    {
        // Ca donne les index au lieu de l'objet
        travJournee.innerHTML += `<option value="${lesTrav[trav].heure}"> ${lesTrav[trav].heure} </option>`
    }
}

// La fonction qui affiche le taux de remplissage
const afficherTab = async () =>
{
    // La liaison choisi par l'utilisateur
    const liaison = document.getElementById("liaison").value;
    // La date choisi par l'utilisateur 
    const date = new Date(document.getElementById("date").value);
    // La traversée choisi par l'utilisateur
    const heureChoisi = document.getElementById("traversee").value;

    // Load le json requit pour les traversées d'une journée
    const infoJourTrav = await getJsonLiaisonDate(liaison, date.getDate());

    for (const traversee in infoJourTrav)
    {
        if (infoJourTrav[traversee].heure == heureChoisi)
        {
            // Les valeurs qu'ils faut man,ipuler pour pouvoir afficher correctement
            const nbPass = infoJourTrav[traversee].nbReservationPassagers;
            const nbPassMax = infoJourTrav[traversee].capacitePassagers;
            const nbVeh = infoJourTrav[traversee].nbReservationVoitures;
            const nbVehMax = infoJourTrav[traversee].capaciteVoitures;

            // Ajouter dans la page HTML avec innerHTML
            // Valeur des passagers
            statAAfficherPass = document.getElementById("ratePass");
            statAAfficherPass.innerHTML = nbPass + "/" + nbPassMax;

            // Valeur des vehicules
            statAAfficherVeh = document.getElementById("rateVeh");
            statAAfficherVeh.innerHTML = nbVeh + "/" + nbVehMax;

            // Mettre la couleur nécessaire pour la case voiture
            caseVoiture = document.getElementById("vehiculeCard");
            
            // La couleur de la case voiture change en dependant du nombre de vehicule
            const TAUX_75Pourcent = 0.75;
            const TAUX_50Pourcent = 0.50;

            if (nbVeh > nbVehMax * TAUX_75Pourcent)
            {
                caseVoiture.classList.add("rouge");
            }
            else if (nbVeh > nbVehMax * TAUX_50Pourcent)
            {
                caseVoiture.classList.add("orange");
            }
            else 
            {
                caseVoiture.classList.add("vert");
            }
            
            // changer l'affichage du titre
            titre = document.getElementById("placeDispoTitre");
            titre.innerHTML = `Voici nos places de réservation disponibles pour le ${date.getDate()} ${date.toLocaleString('fr-FR', {month: 'long'})} ${date.getFullYear()} à ${heureChoisi}`;
        }
    }
}