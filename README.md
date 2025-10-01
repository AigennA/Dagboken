
# Dagboken
En konsolbaserad dagboksapp skriven i C# som låter användare skriva, läsa och hantera dagboksanteckningar.

## Installation
1. Klona repot: `git clone https://github.com/AigennA/Dagboken.git`
2. Öppna projektet i Visual Studio
3. Bygg projektet (Ctrl+Shift+B)
4. Kör programmet (F5)

## Användning
Appen erbjuder följande funktioner:
- Skriva nya anteckningar
- Lista alla anteckningar
- Söka anteckningar på datum
- Söka anteckningar med text
- Uppdatera befintliga anteckningar
- Ta bort anteckningar
- Spara/läs anteckningar till/från fil

## Exempel på I/O
### Skapa ny anteckning
```
Datum (yyyy-MM-dd): 2025-10-01
Text: Min första anteckning
```

### Söka anteckningar
```
Sökord: viktigt
Hittade 2 anteckningar:
2025-09-30 - Detta är en viktig anteckning
2025-10-01 - En annan viktig dag
```

## Teknisk reflektion
Jag valde att implementera appen med följande designbeslut:

1. Datastrukturer:
- Använde både List<DiaryEntry> och Dictionary<DateTime, DiaryEntry> för optimal prestanda
- Dictionary ger O(1) sökning på datum
- List möjliggör enkel iteration och sortering

2. I/O-format:
- JSON-format för filhantering
- Strikt validering av datum och text
- Automatisk sparande vid viktiga operationer

3. Felhantering:
- Robust validering av användarinput
- Tydliga felmeddelanden på svenska
- Separat felhantering för filoperationer

4. Arkitektur:
- Separation av ansvar mellan DiaryService och MenuStub
- Injektion av beroenden för testbarhet
- Konsekvent felhantering genom hela stacken

Appen är byggd med fokus på användarvänlighet och robusthet, samtidigt som den upprätthåller god kodkvalitet och testbarhet.

## Git-checkpoints
1. Init & skelett - repo, projekt, .gitignore, tomma klasser
2. Meny & loop - navigerbar meny + stubbar
3. Lägg till & lista - implementera AddEntry() och ListEntries()
4. Sökfunktion - sök efter datum
5. I/O & felhantering - spara/läs från fil, implementera try/catch
6. Polish & README - input-validering, dokumentation, release v1.0.0

## Release
v1.0.0 - Första stabila versionen med full funktionalitet och dokumentation