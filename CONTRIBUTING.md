# Arbetsregler

## Branches
- Arbeta aldrig direkt på `main`. Varje issue får en egen branch.
- Tilldela dig själv issuen innan du börjar.
- Hämta senaste `main` innan du skapar en ny branch.
- Döp branchen till `#nummer-kort-beskrivning`, t.ex. `#2-skapa-klasserna`. Tack vare `#` blir numret en länk till issuen i PR-titeln och i merge-meddelandet.
- I terminalen måste namnet stå inom citattecken: `git checkout "#2-skapa-klasserna"`.
- Branches tas inte bort efter merge.

## Commits
- Kontrollera att din e-post i `git config user.email` är kopplad till ditt GitHub-konto. Annars syns inte dina commits som dina.
- Commita ofta och i små steg.
- Börja meddelandet med issuens nummer och beskriv sedan vad som gjorts: `#2 Add withdraw method to Account`, inte `#2 fix` eller `update`.
- Commita med `git commit -m "..."` eller via IDE:n. Om Git öppnar en editor tolkas rader som börjar med `#` som kommentarer och tas bort.
- Kod, kommentarer och namn skrivs på engelska. Text som visas för användaren skrivs på svenska.

## Pull requests
- Bygg och kör programmet innan du öppnar PR:en.
- En issue per PR. Håll dem små.
- Minst två godkännanden krävs. Granska inom ett dygn.
- Den som skapat PR:en mergar när den är godkänd, med "Create a merge commit".

## Code review
- Vi granskar varandras kod för att lära oss. Kommentarer handlar om koden, aldrig om personen.
- Föreslå gärna hur något kan bli tydligare eller renare, och förklara varför.
- Godkänn bara kod du har läst och förstår.