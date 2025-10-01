
# Dagboken
En konsolbaserad dagboksapp skriven i C# som l�ter anv�ndare skriva, l�sa och hantera dagboksanteckningar.

## Installation
1. Klona repot: `git clone https://github.com/AigennA/Dagboken.git`
2. �ppna projektet i Visual Studio
3. Bygg projektet (Ctrl+Shift+B)
4. K�r programmet (F5)

## Anv�ndning
Appen erbjuder f�ljande funktioner:
- Skriva nya anteckningar
- Lista alla anteckningar
- S�ka anteckningar p� datum
- S�ka anteckningar med text
- Uppdatera befintliga anteckningar
- Ta bort anteckningar
- Spara/l�s anteckningar till/fr�n fil

## Exempel p� I/O
### Skapa ny anteckning
```
Datum (yyyy-MM-dd): 2025-10-01
Text: Min f�rsta anteckning
```

### S�ka anteckningar
```
S�kord: viktigt
Hittade 2 anteckningar:
2025-09-30 - Detta �r en viktig anteckning
2025-10-01 - En annan viktig dag
```

## Teknisk reflektion
Jag valde att implementera appen med f�ljande designbeslut:

1. Datastrukturer:
- Anv�nde b�de List<DiaryEntry> och Dictionary<DateTime, DiaryEntry> f�r optimal prestanda
- Dictionary ger O(1) s�kning p� datum
- List m�jligg�r enkel iteration och sortering

2. I/O-format:
- JSON-format f�r filhantering
- Strikt validering av datum och text
- Automatisk sparande vid viktiga operationer

3. Felhantering:
- Robust validering av anv�ndarinput
- Tydliga felmeddelanden p� svenska
- Separat felhantering f�r filoperationer

4. Arkitektur:
- Separation av ansvar mellan DiaryService och MenuStub
- Injektion av beroenden f�r testbarhet
- Konsekvent felhantering genom hela stacken

Appen �r byggd med fokus p� anv�ndarv�nlighet och robusthet, samtidigt som den uppr�tth�ller god kodkvalitet och testbarhet.

## Git-checkpoints
1. Init & skelett - repo, projekt, .gitignore, tomma klasser
2. Meny & loop - navigerbar meny + stubbar
3. L�gg till & lista - implementera AddEntry() och ListEntries()
4. S�kfunktion - s�k efter datum
5. I/O & felhantering - spara/l�s fr�n fil, implementera try/catch
6. Polish & README - input-validering, dokumentation, release v1.0.0

## Release
v1.0.0 - F�rsta stabila versionen med full funktionalitet och dokumentation