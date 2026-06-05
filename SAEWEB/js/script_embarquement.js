// Fonction pour get un JSON
const getJsonRes = async (numRes) => {
try{
    const response = await fetch(`https://can.iutrs.unistra.fr/api/reservation/${numRes}`);
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
};

// Fonction pour get un JSON d'un passager
const getJsonPass = async (numRes, numPass) => {
try{
    const response = await fetch(`https://can.iutrs.unistra.fr/api/reservation/${numRes}/passager/${numPass}`);
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
};

// Fonction pour get un JSON de vehicule
const getJsonVeh = async (numRes, numPass) => {
try{
    const response = await fetch(`https://can.iutrs.unistra.fr/api/reservation/${numRes}/vehicule/${numPass}`);
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
};


CreezCarte = async() =>
{
	// Les inputs d'utilisateurs
	const value = document.getElementById("ident").value;
	// Le data sur l'utilisateur (s'il existe)
	const infoReservation = await getJsonRes(value);
	nbPass = infoReservation.nbPassagers;
	nbVeh = infoReservation.nbVehicules;


	// Les passagers et véhicules
	let listeCarte = [];

	// Boucle passager
	for (i = 1; i <= nbPass; i++)
	{
		// get le passager
		const infoPass = await getJsonPass(value, i);

		listeCarte += 
	`<section class="carte">
            <div class="gauche">
                <img class="image_carte" src="carte_img.png">

                    <table class="tab_gauche">
                        <tbody>
                            <tr>
                                <th scope="row">Gare de départ</th>
                                <td>${infoReservation.portDepart}</td>
                            </tr>
                            <tr>
                                <th scope="row">Gare d'arrivée</th>
                                <td>${infoReservation.portArrivee}</td>
                            </tr>
                            <tr>
                                <th scope="row">Date</th>
                                <td>${infoReservation.date}</td>
                            </tr>
                            <tr>
                                <th scope="row">Heure de départ</th>
                                <td>${infoReservation.heure}</td>
                            </tr>
                            <tr>
                                <th scope="row">Bateau</th>
                                <td>${infoReservation.bateau}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="droite">
                    <p>Carte d'embarquement</p>
                    <div class="droite_bloc">
                        <section>
                            <table>
                                <tbody>
                                    <tr>
                                        <th scope="row">Reservation</th>
                                        <td>${value}</td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Nom</th>
                                        <td>${infoReservation.nom}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </section>
                        <hr>
                        <p>Passager</p>
                        <table>
                            <tbody>
                                <tr>
                                    <th scope="row">Nom</th>
                                    <td>${infoPass.nom}</td>
                                </tr>
                                <tr>
                                    <th scope="row">Prénom</th>
                                    <td>${infoPass.prenom}</td>
                                </tr>
								<tr>
                                    <th scope="row">Catégorie</th>
                                    <td>${infoPass.code}</td>
                                </tr>
								<tr>
                                    <th scope="row">Prix</th>
                                    <td>${infoPass.price}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
        </section>`

	}

	// boucle vehicule
	for (i = 1; i <= nbVeh; i++)
	{
		// get le vehicule
		const infoVeh = await getJsonVeh(value, i);

		listeCarte += 
		`<section class="carte">
            <div class="gauche">
                <img class="image_carte" src="carte_img.png">

                    <table class="tab_gauche">
                        <tbody>
                            <tr>
                                <th scope="row">Gare de départ</th>
                                <td>${infoReservation.portDepart}</td>
                            </tr>
                            <tr>
                                <th scope="row">Gare d'arrivée</th>
                                <td>${infoReservation.portArrivee}</td>
                            </tr>
                            <tr>
                                <th scope="row">Date</th>
                                <td>${infoReservation.date}</td>
                            </tr>
                            <tr>
                                <th scope="row">Heure de départ</th>
                                <td>${infoReservation.heure}</td>
                            </tr>
                            <tr>
                                <th scope="row">Bateau</th>
                                <td>${infoReservation.bateau}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="droite">
                    <p>Carte d'embarquement</p>
                    <div class="droite_bloc">
                        <section>
                            <table>
                                <tbody>
                                    <tr>
                                        <th scope="row">Reservation</th>
                                        <td>${value}</td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Nom</th>
                                        <td>${infoReservation.nom}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </section>
                        <hr>
                        <p>Véhicule</p>
                        <table>
                            <tbody>
                                <tr>
                                    <th scope="row">Catégorie</th>
                                    <td>${infoVeh.code}</td>
                                </tr>
                                <tr>
                                    <th scope="row">Nombre</th>
                                    <td>${infoVeh.quantite}</td>
                                </tr>
								<tr>
                                    <th scope="row">Prix</th>
                                    <td>${infoVeh.prix}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
        </section>`

	}

	aller = document.getElementById("containerCarteAller");

	aller.innerHTML = listeCarte;

}

AfficherFacture = async() =>
{
    // Les inputs d'utilisateurs
	const value = document.getElementById("ident").value;
	// Le data (s'il existe)
	const jsonReservation = await getJsonRes(value);
	nbPass = jsonReservation.nbPassagers;
	nbVeh = jsonReservation.nbVehicules;

	// Reset les cartes pour rendre la facture plus lisible
	aller = document.getElementById("containerCarteAller");
	aller.innerHTML = ``;

	// L'entete de la facture
	header = 
	`<div class="facture">

        <header id="ImgFact">
            <img src="CAN.png">
        </header>

        <br>
        <br>

        
        <h1>Facture</h1>

		<!-- Mais comment avoir le vrai horodatage?? --->
        <p class="date">Illkirch le ${"20/10/2025"} </p>
        <p class="infoFact"> Réservation n°: ${value} </p>
        <p class="infoFact"> Nom de réservation : ${jsonReservation.nom} </p>

        <br>
        <br>

		<!-- Comment avoir le vrai idLiaison avec l'API ? --->
        <p class="infoRes"> Traversée : ${jsonReservation.portDepart + " - " + jsonReservation.portArrivee}</p>
        <p class="infoRes"> Date : ${jsonReservation.date} </p>
        <p class="infoRes"> Départ : ${jsonReservation.heure} </p>
        <p class="infoRes"> Bateau : ${jsonReservation.bateau}</p>

        <br>
        <br>`
	
	// Personnes
	personnes = 
		`
		<p class="title">Les personnes</p>
    
        <table id="Prixpers">
            <thead>
                <tr>
                    <th>Catégorie</th>
                    <th>Nombre</th>
                    <th>Prix unitaire</th>
                    <th>Prix ligne</th>
                </tr>
            
            </thead>
            <tbody>`

	// On va faire tous un cirque pour que l'age soit decroissante
	let nomLibelle = {
				  "adu26p": "",
				  "jeu1825" : "",
				  "enf417" : "", 
				  "bebe" : "", 
				  "ancomp": ""
				};
	// Le nombre de personne par catégorie
	let nbCat = {
				  "adu26p": 0,
				  "jeu1825" : 0,
				  "enf417" : 0, 
				  "bebe" : 0, 
				  "ancomp": 0
				};

	// Le prix Unitaire
	let prixUnit = {
				  "adu26p": 0.00,
				  "jeu1825" : 0.00,
				  "enf417" : 0.00, 
				  "bebe" : 0.00, 
				  "ancomp": 0.00
				};

	
	// Premiere boucle pour calculer le nombre de client de chaque catégorie, le prix unitaire et le prix de ligne
	for (i = 1; i <= nbPass; i++)
	{
		// Le json du passager
		const passagerCourant = await getJsonPass(value, i);

		// incremente nombre et prix
		nomLibelle[passagerCourant.code] = passagerCourant.libelleCategorie;
		nbCat[passagerCourant.code] += 1;
		prixUnit[passagerCourant.code] = passagerCourant.price;
	};

	// Le prix total
	let totalPas = 0;
	// Pour parcourir
	const listeCat = ["adu26p", "jeu1825", "enf417", "bebe", "ancomp"];
	// Parcour chaque catégorie et mettre le nombre requis
	for (const cat in listeCat)
	{	
		// "cat" est l'indice et non la valeur pour je ne sait quelle raison
		const categorie = listeCat[cat]

		//console.log(nomLibelle["bebe"]);
		//console.log(prixUnit["bebe"]);

		if (nbCat[categorie] != 0)
		{
			personnes += 
			`<tr>
				<th>${nomLibelle[categorie]}</th>
				<td class="nb">${nbCat[categorie]}</td>
				<td>${prixUnit[categorie].toFixed(2)}</td>
				<td>${(prixUnit[categorie] * nbCat[categorie]).toFixed(2)}</td>
			</tr>`
			
			// Prix total des passagers
			totalPas += prixUnit[categorie] * nbCat[categorie];
		}
	}
	console.log(totalPas);

			
	personnes +=`</tbody>
		</table>
		
		<p id="PrixpersTotal">Sous-total personnes : ${totalPas.toFixed(2) + " €"} </p>`
		


	vehicules =
		`
        <p class="title"> Les véhicules</p>

        <table id="PrixVehic">
            <thead>
                <tr>
                    <th>Catégorie</th>
                    <th>Nombre</th>
                    <th>prix unitaire</th>
                    <th>Prix ligne</th>
                </tr>
            </thead>
		`
	// Prix total des vehicules
	let totalVeh = 0;

	for (i = 1; i <= jsonReservation.nbVehicules; i++)
	{
		const vehiculeCourrant = await getJsonVeh(value, i);

		let quant = vehiculeCourrant.quantite;
		let prixVeh = vehiculeCourrant.prix;
		let prixLigne = prixVeh * quant;

		vehicules += 
		`<tr>
            <th>${vehiculeCourrant.libelle}</th>
            <td class="nb">${quant}</td>
            <td>${(prixVeh).toFixed(2)}</td>
            <td>${(prixVeh * quant).toFixed(2)}</td>
        </tr>`

		// Prix total des voitures
		totalVeh += prixLigne;
	}

	fin =

		`
            
            </tbody>
        </table>

        <p id="PrixVehicTotal"> Sous-total véhicule : ${totalVeh.toFixed() + " €"} </p>

        <p id="PrixTotal"> Prix total : A rajouter ${(totalPas + totalVeh).toFixed() + " €"}</p>
    </div>`;

	// Ajouter dans la zone ou il faut ajouter
	zoneFacture = document.getElementById("containerFacture");

	let laFacture = header + personnes + vehicules + fin;

	zoneFacture.innerHTML = laFacture;
}