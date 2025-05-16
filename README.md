# JuanLog

JuanLog je aplikace urèená specificky pro Juanovo trackování cvièení. Na základì jeho formátu se naèítá z csv.

Juan je defaultním uživatelem, primárnì by mìl mít heslo nastaveno na q nebo w. V Resources je pøiložen soubor JUAN.csv pro testování naèítání Juanových dat ze souboru.
Juanova data mají formát:

Datum	Šòùra	Excercise	Weight (kg)	Reps	Reps	Reps	Reps +	Pozn.


Projekt používá Entity Framework, pro zkrášlení GUI je využito WPF-UI.

## Funkcionality JuanLogu
- [x] Juan se mùže registrovat
- [x] Juan se mùže/musí pøihlásit pro pøístup do aplikace
- [x] Juan si mùže naèíst pøedchozí záznamy o cvièení z excelovského csv
- [x] Juan mùže zapsat jednotlivá cvièení
- [x] Juan má k dispozici kategorie cvièení, ze kterých vybírat
- [x] Juan mùže pøidat nové cvièení
- [x] Juan mùže v tabulce filtrovat, jaká kategorie v èase ho zajímá
- [x] Juan mùže zobrazit svùj postup v èase na grafu
- [x] Juan mùže v grafu filtrovat, jaká kategorie v èase ho zajímá
- [x] Juan má k dispozici heatmapu dní, kdy cvièil/streak
- [x] Juan mùže live editovat cvièení
- [x] Juan mùže live odstraòovat cvièení
- [x] Juan mùže volit z jednotlivých kategorií (bøicho, záda - csv nepodporuje -> doèasnì vše generické)

## TO-DO nad rámec
- [ ] Juan mùže vkládat cvièení o rùzných datech
- [ ] Juanùv graf umí zobrazit poèet cvièení v èase
- [ ] Juan je podporován motivaèními citáty
- [ ] Juanova databáze zpracovává šòùry
- [ ] Juanùv streak bere v potaz dny odpoèinku a nepenalizuje za nì
- [ ] Juan si mùže pøidávat vlastní kategorie pro cviky
- [ ] Juanovo UX je podpoøeno plynulejšími pøechody (napø. boèní navigací)
- [ ] Juanova heatmapka má fancy labels
- [ ] Juan se mùže kochat hezkými ikonkami
- [ ] Juan si mùže sám mìnit barvièky pro interface
- [ ] Juanovo UX je vylepšeno tím, že pøi editaci nemusí znovu vybírat kategorii a cvièení
- [ ] Juanova snaha je podpoøena motivaèními citáty namísto pùvodního greeteru! Yay!

## Done dodatky
- [x] Zamítnuto: Promyslet použití secure stringù pøi pøihlašování
- [x] Hotovo: Promazávat login boxy po pøihlášení XD
- [x] Zamítnuto: Pøekopat databázi, protože ffs XDD -> pøi importu brát jednotlivé sety spíš jako celé entry, bude se s tím líp pracovat
- [ ] Zakázat ne-matching heslo pøi registraci (check)? kinda annoying
- [ ] Pøesunout styly do style dictu, což by proèistilo kód
- [ ] Možná zakázat empty uživatele? ale, to je vìc uživatele
- [ ] Zlepšit dokumentaci, hehe