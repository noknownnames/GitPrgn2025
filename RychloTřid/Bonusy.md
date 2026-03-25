2. Bonus č.2:
    Množinu všech seřazených prvků můžeme rozdělit na čtyři části, jelikož je stejá pravděpodobnost, že náhodně vybereme jakoukoliv hodnotu uvnitř těchto úseků a každý je stejně velký. víme tedy, že hodnoty v prvním a posledním úseku nejsou v rozmezí skoromediánu.

    Aby byl medián tří prvků menší, než skoromedián, musíme zvolit alespoň dvakrát úsek menší než skoromedián. máme tedy jeden(1) případ, kde jsou všechny hodnoty v úseku pod skoromediánem a tři případy, kde se dva prvky nacházejí v oblasti pod skoromediánem, tj: {pod;pod;v 1. pol. skoromediánu}, {pod,pod,ve 2. pol. skoromediánu} a {pod,pod,nad}, kde vždy tři možnosti, jak zaplnit indexy 0,1/2n,n, což nám dává devět(9) možností. Obdobně, jen opačně toto platí pro medián tří prvků větší, než skoromedián a jelikož celkový počet způsobů, jak vybrat úseky s prvky 0,1/2n,n je K'(3,4) = 64 , tak celková pravděpodobnost toho, že se vybraný medián tří prvků *ne*nachází v množině je (2*[9+2])/64 = 5/16. pravděpodobnost toho, že se hodnota nachází ve skoromediánu je tedy 11/16 ≈ 68,75%.  

    To, že pivot bude minimem je jev nemožný, jelikož vždycky, když vybereme náhodně hodnotu z pole, vybereme vždy dvě hodnoty, které nejsou minimem a tudíž medián také nebude minimum.
3. Bouns č.3:
    Vybereme li vždy medián jako pivot, vzniknou maximálně větve s logaritmickou hloubkou rekurze, jelikož jsme pole rozdělili vždy na polovinu.
4. Buons č.4:
    Medián a průměr se bude výrazně lišit u posloupností s prvky, které se výrazně odlišují z celkového trendu(například výšky 20 tučňáků patagonských a 3 tučňáků císařských), nebo poslopností s daty blíže k jednomu extému.