# JuanLog

JuanLog je aplikace ur�en� specificky pro Juanovo trackov�n� cvi�en�. Na z�klad� jeho form�tu se na��t� z csv.

Juan je defaultn�m u�ivatelem, prim�rn� by m�l m�t heslo nastaveno na q nebo w. V Resources je p�ilo�en soubor JUAN.csv pro testov�n� na��t�n� Juanov�ch dat ze souboru.
Juanova data maj� form�t:

Datum	���ra	Excercise	Weight (kg)	Reps	Reps	Reps	Reps +	Pozn.


Projekt pou��v� Entity Framework, pro zkr�len� GUI je vyu�ito WPF-UI.

## Funkcionality JuanLogu
- [x] Juan se m��e registrovat
- [x] Juan se m��e/mus� p�ihl�sit pro p��stup do aplikace
- [x] Juan si m��e na��st p�edchoz� z�znamy o cvi�en� z excelovsk�ho csv
- [x] Juan m��e zapsat jednotliv� cvi�en�
- [x] Juan m� k dispozici kategorie cvi�en�, ze kter�ch vyb�rat
- [x] Juan m��e p�idat nov� cvi�en�
- [x] Juan m��e v tabulce filtrovat, jak� kategorie v �ase ho zaj�m�
- [x] Juan m��e zobrazit sv�j postup v �ase na grafu
- [x] Juan m��e v grafu filtrovat, jak� kategorie v �ase ho zaj�m�
- [x] Juan m� k dispozici heatmapu dn�, kdy cvi�il/streak
- [x] Juan m��e live editovat cvi�en�
- [x] Juan m��e live odstra�ovat cvi�en�
- [x] Juan m��e volit z jednotliv�ch kategori� (b�icho, z�da - csv nepodporuje -> do�asn� v�e generick�)

## TO-DO nad r�mec
- [ ] Juan m��e vkl�dat cvi�en� o r�zn�ch datech
- [ ] Juan�v graf um� zobrazit po�et cvi�en� v �ase
- [ ] Juan je podporov�n motiva�n�mi cit�ty
- [ ] Juanova datab�ze zpracov�v� ���ry
- [ ] Juan�v streak bere v potaz dny odpo�inku a nepenalizuje za n�
- [ ] Juan si m��e p�id�vat vlastn� kategorie pro cviky
- [ ] Juanovo UX je podpo�eno plynulej��mi p�echody (nap�. bo�n� navigac�)
- [ ] Juanova heatmapka m� fancy labels
- [ ] Juan se m��e kochat hezk�mi ikonkami
- [ ] Juan si m��e s�m m�nit barvi�ky pro interface
- [ ] Juanovo UX je vylep�eno t�m, �e p�i editaci nemus� znovu vyb�rat kategorii a cvi�en�
- [ ] Juanova snaha je podpo�ena motiva�n�mi cit�ty nam�sto p�vodn�ho greeteru! Yay!

## Done dodatky
- [x] Zam�tnuto: Promyslet pou�it� secure string� p�i p�ihla�ov�n�
- [x] Hotovo: Promaz�vat login boxy po p�ihl�en� XD
- [x] Zam�tnuto: P�ekopat datab�zi, proto�e ffs XDD -> p�i importu br�t jednotliv� sety sp� jako cel� entry, bude se s t�m l�p pracovat
- [ ] Zak�zat ne-matching heslo p�i registraci (check)? kinda annoying
- [ ] P�esunout styly do style dictu, co� by pro�istilo k�d
- [ ] Mo�n� zak�zat empty u�ivatele? ale, to je v�c u�ivatele
- [ ] Zlep�it dokumentaci, hehe