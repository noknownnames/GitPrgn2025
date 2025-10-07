
# 🛠 Jak probíhá kompilace v C# ve Visual Studiu

## Zdrojový kód (.cs soubory)
- Píšete v jazyce C#.

## Překlad do IL (Intermediate Language)
- Kompilátor jazyka C# (`csc.exe`) přeloží kód do **IL** (někdy také MSIL nebo CIL = Common Intermediate Language).
- Výsledkem jsou **.dll** nebo **.exe** soubory, které ale ještě nejsou „nativní“ strojový kód.

## Uložení do Assembly
Vznikne tzv. **assembly** – tedy balíček obsahující:
- IL kód
- Metadata (informace o třídách, metodách, typech)
- Manifest (popis assembly, závislosti atd.)

## Spuštění a JIT (Just-In-Time) kompilace
- Když program spustíte, **CLR (Common Language Runtime)** vezme IL kód a při běhu ho **JIT kompilátor** překládá do nativního strojového kódu pro daný procesor.
- Díky tomu je kód přenositelný – IL je stejný pro Windows, Linux i macOS, ale JIT přizpůsobí strojový kód konkrétnímu systému.

---

# 🏗️ Co je to Build ve Visual Studiu

**Build** = proces, který provádí:
1. Překlad všech zdrojových souborů do IL kódu.
2. Sestavení výsledného spustitelného souboru (.exe) nebo knihovny (.dll).
3. Zahrnutí závislostí, resource souborů, konfigurace atd.
4. Uložení do složky **bin/Debug** nebo **bin/Release** podle nastavení.

👉 Ve Visual Studiu najdete více možností:
- **Build Solution** – přeloží všechny projekty ve solution, které je potřeba znovu sestavit.  
- **Rebuild Solution** – smaže všechny staré sestavené soubory a přeloží úplně vše od začátku.  
- **Clean Solution** – jen smaže výsledky předchozího buildu (bez překladu).  

---

# 🔹 Co je vlastně ten .exe v bin/Debug?

- Ten .exe soubor (nebo .dll), který najdete v bin/Debug nebo bin/Release, není nativní strojový kód.
- Obsahuje IL (Intermediate Language) + metadata a manifest → tzv. assembly.
- Vypadá jako spustitelný soubor pro Windows, ale ve skutečnosti to není „hotový binární kód“ jako třeba program zkompilovaný v C++.

## 🔹 Lze ten soubor sdílet mezi platformami?

- Ano, ale s podmínkou:
- Pokud jste program zkompiloval v .NET (Core / 5+), pak .dll nebo .exe obsahující IL můžete spustit i na Linuxu nebo macOS (pokud tam je nainstalované .NET runtime).
- Pokud je to .NET Framework (klasický, jen pro Windows), pak to půjde spustit pouze na Windows, protože ten runtime existuje jen tam.

---

# 🔑 Shrnutí:
- **Kompilace C#** má dvě fáze:  
  1. C# → IL (při buildu)  
  2. IL → strojový kód (při spuštění, JIT kompilace).  

- **Build** je v podstatě automatizovaný proces té první fáze (vytvoření IL kódu a sestavení assembly).

- Používejte **.NET (Core / moderní .NET)**, který je multiplatformní.
