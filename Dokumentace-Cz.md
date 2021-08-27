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

## Instalace
Pro sestavení projektu je třeba mít nainstalován program Visual Studio 2019 s
rozšířením pro programovací jazyk C# a okenní aplikace Windows Forms. Zde je
pak jen potřeba zvolit `Build -> Build Solution` a program již bude možné 
spustit.

## Hlavni Menu
Program po spuštění zobrazí okno, na němž je vyobrazeno hlavni menu, kde je na
výběr ze 4 tlačítek. Tlačítko `Play` spustí novou hru. Volbou `Load Objects` 
(resp.`Load Map`) přejde program do menu pro načítání vlastních herních objektů,
(resp. vlastní podoby počátečního hracího pole). Nakonec volba `End Game`
ukončí program (program lze také kdykoliv ukončít stiskem klávesy `Esc`).

## Načítání herních objektů
Program je nastaven tak, že reprezentace defaultních herních bloků je uložena v
textovém souboru s relativní adresou `./GameObjects/Default_Objects.txt`. A 
uživatel tak nemusí načítat herní objekty, pokud chce použít původní verzi.
Pokud ale má uživatel zájem využít vlastní sadu herních objektů, musí 
postupovat, jak je popsáno níže.

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
Podobně jako v případě hernních objektů není nutno pro defaulní nastavení hry
načítat herní mapu (ta je defaultně složena z prázdných políček). Pokud má
ale uživatel zájem použít vlastní počáteční verzi mapy, musí postupovat dle 
popisu níže.

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

## Hrací pole a Ovládání
Po volbě `Play` je zobrazeno hrací pole a je hráči umožněno pohybovat padající
kostičkou. Je zde také vyobrazeno aktuální skóre a následující blok.


### Ovládání
Pohyb padající kostičky je realizován stiskem dané klávesy. Pro posun objektu
doleva je třeba stisk klávesy `Left` nebo `A`, doprava `Right`, `D`, pro 
urychlení pádu `Down`, `S` a pro rotaci objektu `Up`, `W` nebo `Space`. Pohyb
není umožněn, pokud by nastala kolize s hranicí hracího pole nebo s již 
položeným blokem.

Kostička samovolně padá a rychlost pádu se zvětšuje s narůstajícím skóre.

## Konec hry
Hra končí, pokud je položen blok a násleující objekt, již nelze na hrací plochu
umístit, aniž by kolidoval s hranicí hrací plochy, či již položenými bloky. V 
takovém připadě se zobrazí nápis `Game Over` společně s možnostmi `Play` a 
`End Game`, jejichž funkcionalita je popsána v odstavci Hlavní Menu.


# Programátorská dokumentace
Hra je napsána v jazyce C# za použití Windows Forms. K zobrazování oken hry je
za pomocí typu `enum` s názvem `GameState`, který slouží především k popisu
aktuálního stavu programu a vhodném zobrazení okna (tlačítek, textu apod.).
Jedná se o `START` symbolizující počáteční menu, `LOADING_BLOCKS` menu pro
načítání herních bloků, `LOADING_MAP` menu pro načítání mapy, `ERROR_BLOCKS` 
menu v případě chybně načtených herních bloků, `ERROR_MAP` menu pokud je špatně
načtena mapa, `GAME` hlavní herní cyklus a `END` nabídka po konci hry.

## Třída Window
Slouží především pro manipulaci s oknem aplikace. Nachází se zde veškerá 
logika spojená s vyobrazováním nápisů tlačítek i s vykreslovanám hracího pole.

### Objekty třídy Timer
Ve třídě `Window` jsou použity 2 objekty třídy `Timer` sloužící pro práci s
časem.

Objekt `timerFalling` slouží k detekci času pro další pád herního 
objektu. Během každého signálu je volána funkce `timerFalling_Tick()`, jež 
zkontroluje zda je možné posunout herním objektem o jedno políčko dolů a 
pokud ano, tak také akci provede. Pokud to možné není, pak zapíše bitmapu bloku
do bitmapy pro reprezentaci herního pole, zkontroluje zda lze do mapy umístit 
následující objekt (zda nenastal konec hry) a provede tak (nebo ukončí hru).
Navíc také kontroluje, zda byla zaplněna nějaká řada na mapě. Pokud ano, tak
provede příslušné operace (náležitě aktualizuje mapu a skóre).

Objekt `timerHandling` slouží pouze jako signál pro program, že uběhlo dostatek 
času pro další uživatelský vstup (slouží především jako prevence špatně 
detekovaného signálu stisku klávesy). Signál objektu pouze změní proměnnou 
`handlingAvailable` a zpřístupní tak uživateli ovládání hry.

### Funkce tvaru 'draw`
Funkce začínající názvem `draw` vykreslují buď herní objekt, nebo mapu. Pracují
s objekty třídy `Bitmap` a `Graphics` a velikost ovlivňují proměnné `mapWidth`,
`mapHeight` a `dotSize`, kde poslední jmenovaná určuje velikost jednoho políčka
hracího pole. Funkce typu `draw` pouze procházejí danou bitmapu a dle hodnoty
aktuální pozice vyplní herní políčko příslušnou barvou (zaplněno/nezaplněno).

### Ostatní
Zbytek funkcí slouží především pro koordinaci funkcí jiných objektů, které jsou
součástí třídy `Window`. Slouží například k načítání objektů a mapy, ke 
kontrole herních stavů (konec hry, zaplnění řady...), spuštění nové hry 
(a resetování stavů), koordinaci funkce tlačítek či stisků kláves (pro 
ovládání hry).

## Třída PlayGround
Slouží jako uživatelské rozhraní herní logiky, zapouzdřuje veškeré herní 
objekty a mapu, stará se o pohyb herního bloku, generování nových bloků a 
načítání herních objektů. Herní bloky jsou zde uloženy v poli o pevné 
velikosti (objekty třídy `Block` slouží pouze jako reprezentace tvaru 
herního objektu, tedy pro každý druh je využit právě 1 objekt této třídy).

Obsahuje funkce:
- `GenerateNextBLock()` - náhodně zvolí následující herní objekt a vypočítá
jeho pozici (aby kostička byla co při vložení do herního pole co nejvíce 
uprostřed).
- `CheckGameOver()` - zkontroluje, zda lze následující kostičku položit do
hracího pole (konec hry), pokud ano generuje také následující herní blok
- `MoveLeft()`, `MoveRight()`, `MoveDown()` - zkontroluje, zda je to možné a
pokud ano, tak posune herním blokem (doleva, doprava nebo dolů)
- `Rotate()` - zkontroluje, zda je to možné a pokud ano, pak zrotuje objekt

## Třída GameObject
Abstraktní třída zapouzdřující společné prvky herního objektu a mapy, mezi nějž
patří šířka, výška a bitmapová reprezentace objektu. Obsahuje také funkci 
`AddBlock()`, která přidává jednu zaplněnou pozici do bitmapové reprezentace 
(především pro načítání, případně přidávání objektu do mapy).

## Třída Block
Potomek třídy `GameObject` přidávající funkcionalitu spojenou s rotací objektu.
Používá výčtový typ `Rotation` pro určení v jaké pozici se objekt nachází
(o kolik je zrotován vůči původní pozici (varianta `Bitmap`)). Pro efektivnější
zrotování objektů obsahuje 3 další bitmapy (`Bitmap90`, `Bitmap180`, 
`Bitmap270`) pro každou variantu rotace. Ty jsou inicializovány voláním funkce
`InitRotationShapes()`, která má být volána až po úplné inicializaci původní 
bitmapy objektu (jinak nebudou inicializovány správně). Toto řešení není 
významně paměťově náročné, protože objekty třídy `Block` jsou používány pouze k
uložení reprezentace různých druhů objektů a herní cyklus pak pracuje pouze s
referencemi na tyto objekty, proto bude objektů třídy `Block` pouze tolik, 
kolik existuje různých druhů herních bloků (tedy jednotky).

Další funkce jsou:
- `GetBitmapToCheck()` - vrátí referenci na bitmapu pro aktuální rotaci
- `RotateLeft()`, `RotateRight()` - "zrotuje" objekt (nastaví parametry tak, 
aby reprezentace objektu odpovídala zrotování objektu)

## Třída Map
Potomek třídy `GameObject` přidávající funkcionality spojené s práci s mapou.
Spravuje také aktuální skóre hry `score` a je zde kromě mapy samotné uložena
reprezentace počáteční mapy (pro opakované spouštění hry).

Hlavní funkce:
- `SetActualDefault()` - nastaví aktuální bitmapu jako počáteční podobu 
(pro restart hry)
- `AddObject()` - přidá zvolený herní objekt do mapy na danou pozici (změní 
bimapu herního pole)
- `ResetGame()` - vynuluje skóre a nastaví bitmapu na počáteční (definovanou 
v `SetActualDefault()` )
- `CheckBlockPossible()` - zkontroluje, zda lze zvolený blok umístit na mapu na 
danou pozici (nekoliduje s hranicí mapy ani s již položenými bloky na mapě)
- `ManageFilledRows()` - zkontroluje, zda se na mapě nachází zaplněná řada, 
pokud ano, posune horní část mapy o 1 pozici níže a náležitě aktualizuje skóre,
nejvyšší patro mapy zaplní prázdnými kostičkami

## Třída Reader
Slouží k načítání reprezentace herních objektů nebo mapy. Obsahuje proměnnou
`Error` sloužící jako příznak, zda během načítání nastala chyba (`true` - chyba
nastala, `false` - chyba nenastala). Dále na místě proměnné `textReader` je
uložen objekt třídy `TextReader` sloužící k přístupu ke vstupnímu souboru a 
`lastLine`, kde je uložen poslední přečtený řádek vstupu (typicky po volání
funkcí je zde načten následující řádek vyžadující další zpracování). Jsou zde
nabízeny také 2 varianty konstruktoru. První varianta bere jako parametr jméno
vstupního souboru, proměnná `textReader` je pak objektem třídy `StreamReader` 
pro načítání vstupu ze souboru a jedná se o variantu, která je typicky užívána
v programu. Druhá varianta je, že je konstruktor volán s parametrem třídy 
`TextReader`, který je následně uložen do proměnné `textReader`, toto řešení je
přizpůsobeno především pro testování pomocí objektů třídy `StringReader`.

Hlavní funkce jsou:
- `ReadAllBlocks()` - načte všechny reprezentace herních bloků (případně předá 
informaci o chybě načítání)
- `ReadMap()` - načte herní mapu (musí být stejné velikosti jako je udáno ve 
třídě `Window`) 
- `skipNonSetup()` - přeskočí veškeré komentáře a prázdé řádky a načte do 
proměnné `lastLine` následující řádek obsahující funkčí informaci
- `readNumBlocks()` - načte celkový počet herních bloků a vytvoří příslušně 
pole herních objektů
- `readBlock()` - načte 1 herní objekt

## Testovací data
Pro správnou funkčnost třídy `Reader` a dopočítání podoby zrotovaných objektů
slouží 2 testovací třídy projektu MSTest s názvy `ReaderTests` a `BlockTests`, 
které lze spustit programem Visual Studio 2019 volbou `Test -> Run All Tests`.





