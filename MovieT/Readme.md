3e sprint:

## Database (SSMS)
In deze sprint heb ik de database verder uitgebreid met nieuwe tabellen voor Seizoen en Aflevering. 
Hiermee kan een serie meerdere seizoenen bevatten en kan elk seizoen meerdere afleveringen hebben. 
Daarna heb ik testdata toegevoegd voor verschillende series, 
seizoenen en afleveringen inclusief titel, afleveringsnummer, duurtijd en jaartal.

## Visual Studio Projectstructuur
Ik heb de applicatie verder uitgebreid volgens de 3-lagenstructuur. 
Hiervoor heb ik nieuwe DTO’s, repositories, services, controllers, viewmodels 
en views aangemaakt voor seizoen en aflevering. 
Hierdoor blijft de applicatie overzichtelijk en gescheiden in lagen.

## FR-07 Account aanmaken
Ik heb functionaliteit gebouwd waarmee gebruikers een account kunnen aanmaken. 
Hierbij wordt gecontroleerd of de gebruikersnaam uniek is en of het wachtwoord minimaal 8 tekens bevat. 
Ook moet het bevestigingswachtwoord overeenkomen met het eerder ingevoerde wachtwoord.

## FR-08 Inloggen
Ik heb een loginfunctionaliteit toegevoegd waarbij gebruikers alleen kunnen inloggen met een bestaande 
gebruikersnaam en het juiste wachtwoord.

## FR-09 Seizoenen en afleveringen bekijken
Ik heb functionaliteit toegevoegd waarmee gebruikers op de detailpagina van een serie 
de beschikbare seizoenen kunnen bekijken. Vervolgens kunnen zij per seizoen de afleveringen 
openen en details bekijken zoals titel, afleveringsnummer, duurtijd, aantal afleveringen en jaartal.

## NFR-03 Beveiliging wachtwoorden
Ik heb gewerkt aan het veilig opslaan van wachtwoorden in de database door gebruik te maken van hashing. 
Hierdoor worden wachtwoorden niet als platte tekst opgeslagen en zijn gebruikersgegevens beter beveiligd.

## Analyse en documentatie
Daarnaast heb ik mijn ERD en UML class diagram aangepast aan de nieuwe requirements.
Ook heb ik feedback verwerkt op mijn analysedocument en use cases, testcases en foutmeldingen uitgebreid voor de nieuwe functionaliteiten.
