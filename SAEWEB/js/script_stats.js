const getJsonChiffreAffaire = async (numero) =>
{
    try{
        const response = await fetch(`https://can.iutrs.unistra.fr/api/liaison/${numero}/chiffreAffaire`);
        //Vérifier si la réponse est correcte
        if(!response.ok) {
            throw new Error('Erreur de réseau !');
        }

        //Sinon Retourner le résultat au format JSON
        const data = await response.json();

        return data;
    } 
    catch (error) // Gérer les erreurs
    { 
        console.error('Une erreur est survenue :', error);
        return null;
    }
}

const NB_LIAISON = 4;
const NB_PARAMETRE = 4;

const afficherChiffreDAffaire = async () =>
{
    // Les totaux
    let totalPassCA = 0;
    let totalNbPass = 0;
    let totalVehCA = 0;
    let totalNbVeh = 0;

    // Parcours les liaisons
    for (let i = 1; i <= NB_LIAISON; i++)
    {
        statLiaison = await getJsonChiffreAffaire(i);

        // Parcours les parametress
        for (let y = 1; y <= NB_PARAMETRE; y++)
        {
            cellule = document.getElementById(`${i}${y}`);
            console.log(totalPassCA);
            switch (y)
            {
                // toFixed => 2 nombre aprés la virgule
                // Passager : chiffre d'affaire
                case 1:
                    cellule.innerHTML = statLiaison.passagers.chiffreAffaire.toFixed(2) + " €";;
                    totalPassCA += statLiaison.passagers.chiffreAffaire;
                    break;
                // Passager : nombre de passager
                case 2:
                    cellule.innerHTML = statLiaison.passagers.nombre;
                    totalNbPass += statLiaison.passagers.nombre;
                    break;
                // Vehicule : chiffre d'affaire
                case 3:
                    cellule.innerHTML = statLiaison.vehicules.chiffreAffaire.toFixed(2) + " €";;
                    totalVehCA += statLiaison.vehicules.chiffreAffaire;
                    break;
                // Vehicule : nombre de passager
                case 4:
                    cellule.innerHTML = statLiaison.vehicules.quantite;
                    totalNbVeh += statLiaison.vehicules.quantite;
                    break;
            }
            
        }
    }

    // Afficher le total
    for(let i = 1; i <= NB_PARAMETRE; i++)
    {
        cellule = document.getElementById(`5${i}`);
        switch (i)
        {
            // toFixed => 2 nombre aprés la virgule
            // Passager : chiffre d'affaire
            case 1:
                cellule.innerHTML = totalPassCA.toFixed(2) + " €";
                break;
            // Passager : nombre de passager
            case 2:
                cellule.innerHTML = totalNbPass;
                break;
            // Vehicule : chiffre d'affaire
            case 3:
                cellule.innerHTML = totalVehCA.toFixed(2) + " €";
                break;
            // Vehicule : nombre de passager
            case 4:
                cellule.innerHTML = totalNbVeh;
                break;
        }
    }

    CAglo = document.getElementById(`CAGlobal`);
    
    CAglo += `<h2 id="CAGlobal">Chiffres d'affaires global pour novembre 2025 : ${(totalVehCA+totalPassCA).toFixed(2)}</h2>`;
}

// Execute automatiquement des que la page est ouverte
afficherChiffreDAffaire();
//getJsonChiffreAffaire(3);