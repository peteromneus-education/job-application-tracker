# Job Application Tracker

---
typ:	inlämningsuppgift
kurs:	Objektorienterad programmering, grund
namn:	Peter Omnéus
datum:	2026-10-13
---

## Projektbeskrivning

Det här är **Inlämningsuppgift 1** i kursen 'Objektorienterad programmering, grund'.

Projektet är en prototyp till en Console-applikation som ska kunna användas som ett verktyg för att lagra och följa upp information om jobbansökningar. 

Programmet kan inte lagra information i lagringsminnet i sitt initiala utförande, men detta vore ett naturligt nästa steg i den eventuella utveckligen av projektet.

## Funktioner

Programmet navigeras genom menyval i en huvudmeny och dess undermenyer.

### Funktioner i korthet
- Registrering av ansökningar
- Statusändring
- Borttagning
- Filter
- Sortering
- Statistik

## Körinstruktion

Öppna en terminal i projektmappen och kör `dotnet run`. Kräver .NET 10 SDK.

Vid programstart skapas åtta fiktiva exempelansökningar. Ändringar finns bara i arbetsminnet och försvinner när programmet avslutas.

## Checklista för uppgiftskraven

Här har jag kryssat av uppgifterna från uppgiftskraven på Learnpoint och lagt till förklaringar där det behövdes.

### Kravspecifikation (Programdel)

#### Klass: JobApplication

##### Attribut:
- [x] CompanyName | string 
- [x] PositionTitle | string
- [x] Status | enum - (Applied, Interview, Offer, Rejected)
- [x] ApplicationDate | DateTime - Datum när ansökan skickades
- [x] ResponseDate | DateTime? - Datum när svar mottogs
- [x] SalaryExpectation | int - Önskad lön i kronor

##### Metoder:

- [x] GetDaysSinceApplied() – returnerar antal dagar sedan ansökan skickades.
- [x] GetSummary() – returnerar en kort sammanfattning av ansökan.

#### Klass: JobManager

##### Attribut:

- [x] Applications | List<JobApplication> - Samling av alla ansökningar

##### Metoder:

- [x] AddJob() – lägger till en ny ansökan
- [x] UpdateStatus() – ändrar status på en befintlig ansökan
- [x] ShowAll() – visar alla ansökningar
	> Här har jag lagt till en undermeny där man kan välja filter och sortering
- [x] ShowByStatus() – filtrerar med LINQ efter status (VG del)
	> Ändrat namn till FilterByStatus
	> Använder ett menyval från Main
- [x] ShowStatistics() – visar statistik med LINQ (Count, Average, OrderBy,Where) (VG del)

##### Extra metoder:

- ConfirmAddApplication
	> Låter användaren bekräfta informationen innan den läggs till i listan
- DeleteApplication
	> Metoden för att radera ansökningar
- ShowUnansweredApplications
	> Visar ansökningar som behöver följas upp

#### Meny (krav)

Programmet ska innehålla minst följande alternativ:

- [x] Lägg till ny ansökan
- [x] Visa alla ansökningar
- [x] Filtrera ansökningar efter status (LINQ) (VG del)
	> Ligger som en undermeny till ShowAll
- [x] Sortera ansökningar efter datum (OrderBy) (VG del)
	> Ligger som en undermeny till ShowAll
- [x] Visa statistik:
	> All statistik ligger under huvudmenyval 5
- [x] Totalt antal ansökningar
- [x] Antal per status (VG del)
- [x] Genomsnittlig svarstid (VG del)
- [x] Uppdatera status på en ansökan
- [x] Ta bort en ansökan
- [x] Avsluta programmet

### LINQ (krav) (VG del)

- [x] Minst 3 LINQ-operationer ska användas i projektet.
	> Dessa ligger främst under metoderna för filtrering, sortering och statistik

### GitHub & Versionshantering (krav)

> Git infördes **efter** att huvuddelen av programmet hade skrivits. Den befintliga koden delades därefter upp i separata brancher och pull requests för ansökningsmodellen, hanteringen och konsolgränssnittet.

- [x] Minst 3 grenar (branches)
- [x] Minst 3 Pull Requests
- [x] Branch Protection Rules ska vara aktiverade för main-grenen
Ingen push direkt till main utan PR.
- [x] Beskrivningar i PRs – varje Pull Request ska ha en kort sammanfattning av vad som lagts till/förändrats.
- [x] Minst 5 commits totalt, med tydliga meddelanden (Added JobManager class, Implemented LINQ filters, etc.).

#### README.md ska finnas i repot, med:

- [x] Projektbeskrivning
- [x] Hur man kör programmet
- [x] Ditt namn och datum och reflektion (frågor finns längre ner här)

### Bonusnivå (VG)

För högre betyg:

- [x] Använd färger i konsolen (t.ex. grön = Offer, röd = Rejected)
	> Använder färgsättning för olika typer av Console output
- [x] ShowByStatus() – filtrerar ansökningar med LINQ efter status
- [x] ShowStatistics() – visar ansökningar statistik med LINQ (Count, Average, OrderBy, Where)
- [x] Skapa ett extra filter, t.ex. “Visa ansökningar utan svar äldre än 14 dagar”
- [x] Implementera enkel felhantering (t.ex. ogiltig input)
	> Ligger som diverse "validate"-metoder i `Utilities`-klassen
- [x] Strukturera klasserna i separata filer för extra ren kod

### Reflektionsfrågor (för inlämning)

> Se [reflektionerna](reflektioner.md).
