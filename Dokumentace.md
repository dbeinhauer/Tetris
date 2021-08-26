# Popis Hry
Program je obdobou hry Tetris. Jedná se o hru jejíž cílem je na sebe postupně
pokládat padající objekty složené z malých čtverečků tak, aby čtverečky
zaplnily celou horizontální řadu hracího pole. Hráč má dovoleno pouze posunovat
padajícím objektem doleva (resp. doprava), přetočit objekt o 90 stupňů ve směru
hodinových ručiček (možno i opakovaně), nebo může hráč urychlit pád objektu. 
Po úplném zaplnění horizontální řady zmíněná řada zmizí a veškeré objekty výše
se posunou o 1 řadu níže. Za každou zaplněnou řadu dostává hráč bod, zároveň
se ale také zvýší rychlost padání hracího objektu. Hra končí pokud nejvýše
položená kostička (část objektu) přesáhne danou výškovou hranici 
(překročí hrací pole).


# Uživatelská dokumentace
## Hlavni Menu
Program po spuštění zobrazí okno, na němž je vyobrazeno hlavni menu, kde je na
výběr ze 4 tlačítek. Tlačítko `Play` spustí novou hru. Volbou `Load Objects` 
(resp.`Load Map`) přejde program do menu pro načítání vlastních herních objektů,
(resp. vlastní podoby počátečního hracího pole). Nakonec volba `End Game`
ukončí program (program lze také kdykoliv ukončít stiskem klávesy `Esc`).

## Načítání herních objektů
Po volbě `Load Objects` se zobrazí menu s volbou `Load Objects`, která otevře
průzkumník souborů a vyzve uživatele k volbě souboru s reprezentací objektů
(formát souboru je popsán níže). Také je možné zvolit tlačítko `Return`, 
které znovu vrátí program do Hlavního Menu.

Nastane-li problém s načítáním souboru zmizí již možnost `Return` a nahradí ji
možnost `End Game` (funkce viz. Hlavní menu).

### Soubor s reprezentací herních objektů
Soubor se dělí na 3 části: komentáře, počet objektů a samotná reprezentace 
jednotlivých objektů

#### Komentáře
Jsou řádky souboru začínající znakem `%`. Pozor! Varianta se začínajícími 
mezerami není přípustná a vede k chybnému formátu souboru. Jsou ignorovány 
společně s řádky složenými jen z bilých znaků.

Mohou se vyskytovat v libovolné části souboru s vyjímkou reprezentace objektů.
Tato část vyžaduje striktní formát a případné komentáře a prázdné řádky mohou
způsobit chybu načítání souboru.

#### Počet Objektů
Jedná se o samostatný řádek předcházející všem reprezentacím objektů, jehož
jediným obsahem je číslo udávající počet herních objektů pro načtení. Řádek
nemůže obsahovat žádné jiné znaky s vyjímkou bílých znaků (jsou ignorovány).

#### Reprezentace objektů
Po informaci o počtu objetů obsahuje soubor jednotlivé reprezentace objektů,
kterých je přesně načtený počet. Tato reprezentace se skládá z hlavičky a 
btimapy objektu.

##### Hlavička herního objektu
Hlavička herního objektu je jeden samostatný řádek složen z 2 čísel 
oddělených mezerou. První číslo reprezentuje šířku objektu, 2. číslo udává
výšku objektu. Řádek nesmí obsahovat jiné znaky (s vyjímkou bílých znaků), 
v opačném případě nastane chyba načítání vstupu.

##### Bitmapa herního objektu
Po načtení hlavičky objektu následuje bezprostředně bitmapová reprezentace 
herního objektu. Skládájící se z tolika řádků, jako je výška objektu, a každý
z řádků je složen z právě tolika znaků, jako je šířka objektu.

Jediné dva povolené znaky jsou `.` reprezentující prázdnou kostičku a `#`
značící obsazenou kostičku (část objektu). Jakékoli porušení formátu vede k 
chybnému načtení souboru.

#### Příklad souboru
Následující příklad ukazuje validní formát souboru pro načtení 2 herních objetů.
```
% num_of_blocks
% width height
% Bitmap reprezentation ('.' - empty cell, '#' - filled cell)
% At least one empty line to separate game objects.

2

3 2
###
#..

4 1
####
```

## Načítání mapy
Po volbě `Load Map` se zobrazí menu s volbou `Load Map`, která otevře
průzkumník souborů a vyzve uživatele k volbě souboru s reprezentací mapy
(formát souboru je popsán níže). Také je možné zvolit tlačítko `Return`, 
které znovu vrátí program do Hlavního Menu.

Nastane-li problém s načítáním souboru zmizí již možnost `Return` a nahradí ji
možnost `End Game` (funkce viz. Hlavní menu).

### Soubor s reprezentací mapy
Formát souboru je podobný jako pro reprezentaci herních objektů 
(viz. Soubor s reprezentací herních objektů). Skládá se ale pouze jen z 
komentářů a 1 bitmapy pro reprezentaci mapy, jejíž šířka je  16 a výška 20 
znaků.

#### Příklad souboru
```
% Bitmap reprezentation ('.' - empty cell, '#' - filled cell)
% size 16 20

................
................
................
................
................
................
................
................
................
................
................
................
................
................
................
................
................
...#########....
.#..#...#....##.
#..#..###...#...
```

## Hrací pole
