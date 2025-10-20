# Guía de Estudio — Iterator & Composite (Head First Design Patterns)
**Capítulo:** *The Iterator and Composite Patterns: Well-Managed Collections*  
**Tema:** RTG Casino Online - Gestión de Catálogos y Categorías  
**Objetivo:** Dominar los conceptos de Iterator y Composite aplicados a la industria del gaming RTG, cuándo aplicarlos, sus beneficios, riesgos, y cómo combinarlos para gestionar colecciones y jerarquías sin acoplamiento.

---

## 📚 **Recursos Complementarios de Aprendizaje**

Esta guía de estudio es parte de un conjunto completo de materiales educativos:

- **[📋 Guía Interactiva](IteratorCompositeDemo/INTERACTIVE_DEMO_GUIDE.md)** - Descripción completa del demo interactivo
- **[💻 Soluciones de Práctica](CSharp_Practice_Solutions.cs)** - Código completo con ejemplos funcionando
- **[🎮 Demo Interactivo]** - Ejecuta `dotnet run` para la experiencia completa

### 🎯 **Cómo usar esta guía:**
1. **Estudia** → Esta guía para fundamentos teóricos
2. **Practica** → [Soluciones de Práctica](CSharp_Practice_Solutions.cs) para codificación
3. **Experimenta** → Demo interactivo para ver los patrones en acción
4. **Profundiza** → [Guía Interactiva](IteratorCompositeDemo/INTERACTIVE_DEMO_GUIDE.md) para presentaciones

---

## 1) Resumen ejecutivo
- **Problema:** El cliente termina sabiendo si los juegos están en `List`, `Array` o un árbol de categorías y duplica bucles/condiciones → rompe **OCP** y **encapsulación**.
- **Iterator:** Proporciona una forma **uniforme** de recorrer juegos de casino RTG **sin exponer** la representación interna de cada proveedor.
- **Composite:** Permite modelar **categorías de juegos (árboles)** tratando **Juego Individual** y **Categoría** de manera **uniforme**.
- **Juntos:** Iterator recorre las categorías Composite de manera segura; el cliente realiza operaciones (mostrar, filtrar por RTP, buscar por proveedor) con código simple.

---

## 2) Iterator — Conceptos clave aplicados al Casino RTG
**Intención:** "Proveer un modo estándar de recorrer catálogos de juegos RTG sin exponer su estructura interna."  
**Participantes aplicados al Gaming RTG:**
- `IIterator<CasinoGame>`: Define operaciones de recorrido (p. ej., `HasNext()`, `Next()`).
- `IAggregate<CasinoGame>`: Define `CreateIterator()` para devolver un iterador de juegos.
- Concretos: `SlotsIterator`, `TableGamesIterator`, `SlotsCatalog`, `TableGamesCatalog`.

**Beneficios en el contexto de Casino RTG:**
- **Uniformidad:** Mismo código para recorrer slots RTG (List) y juegos de mesa (Array).
- **Encapsulación:** Oculta si los juegos RTG vienen de base de datos, archivos XML, o APIs REST.
- **OCP:** Nuevos catálogos de juegos RTG solo requieren su iterador específico.
- **Escalabilidad:** Fácil agregar nuevas series como Real Series, i-Slots, etc.

**Decisiones de diseño para Gaming RTG:**
- **Orden de recorrido:** por popularidad, RTP descendente, serie de juego, alfabético...
- **Filtros:** por serie RTG, categoría, RTP mínimo, apuesta mínima...
- **Consistencia:** ¿qué pasa si se agregan juegos RTG durante la iteración?

**Pseudocódigo C# para Casino RTG:**
```csharp
public interface IIterator<T> {
    bool HasNext();
    T Next();
}

public interface IAggregate<T> {
    IIterator<T> CreateIterator();
}

// Cliente uniforme para cualquier catálogo RTG
void PrintGameCatalog(IIterator<CasinoGame> it) {
    while (it.HasNext()) {
        var game = it.Next();
        Console.WriteLine($"🎮 {game.Name} - RTP: {game.RTP}% | Min: ${game.MinBet}");
    }
}
```

**Cuándo usarlo en Gaming RTG:**
- Múltiples series de juegos RTG con diferentes estructuras de datos.
- Quieres exponer un recorrido **seguro** sin revelar APIs internas.
- Necesitas filtros complejos (RTP alto, juegos nuevos, jackpots progresivos).

---

## 3) Composite — Conceptos clave aplicados al Casino RTG
**Intención:** "Componer juegos RTG en **categorías jerárquicas** y permitir tratar **juegos individuales** y **categorías** de forma uniforme."  

**Estructura aplicada al Gaming RTG:**
- `GameComponent` (base): operaciones comunes (`Display()`, `Add`, `Remove`, `ShowHighRTPGames()`).
- `CasinoGame` (Leaf): juego individual RTG (Doragon's Gems, Whispers of Seasons, etc.).
- `GameCategory` (Composite): contiene juegos y subcategorías (Slots → Promotional Games).

**Beneficios en Casino RTG:**
- **Cliente simple:** No distingue entre juego RTG individual y categoría completa.
- **Escalabilidad:** Operaciones recursivas (mostrar toda la estructura, filtrar por RTP).
- **Flexibilidad:** Fácil reorganizar: mover juegos promocionales dentro de slots.

**Ejemplo de jerarquía de Casino RTG:**
```
🎰 RTG CASINO
├── 🎮 SLOT GAMES (Real Series Video Slot games)
│   ├── 🎯 Doragon's Gems
│   ├── 🎯 Whispers of Seasons
│   ├── 🎯 Plentiful Treasure
│   ├── 🎯 Spirit of the Inca
│   └── 🎁 PROMOTIONAL GAMES
│       ├── 🎯 Alien Wins
│       ├── 🎯 Horseman Prize
│       └── 🎯 Fu Long Plinko
├── 🃏 TABLE GAMES (Card and Table Games)
│   ├── 🎯 Blackjack
│   ├── 🎯 European Roulette
│   ├── 🎯 Baccarat
│   ├── 🎯 Texas Hold'em Poker
│   └── 🎯 Craps
└── 🎪 LIVE CASINO (Games with Real Dealers)
    ├── 🎯 Live VIP Blackjack
    ├── 🎯 Live Roulette
    └── 🎯 Live Baccarat
```

**Pseudocódigo C# para Casino RTG:**
```csharp
public abstract class GameComponent {
    public virtual string Name => throw new NotSupportedException();
    public virtual decimal RTP => throw new NotSupportedException();
    public virtual void Add(GameComponent c) => throw new NotSupportedException();
    public virtual void Display() => throw new NotSupportedException();
}

public class CasinoGame : GameComponent {
    public override string Name { get; }
    public override decimal RTP { get; }
    public override void Display() => Console.WriteLine($"🎮 {Name} - RTP: {RTP}%");
}

public class GameCategory : GameComponent {
    private List<GameComponent> _games = new();
    public override void Add(GameComponent c) => _games.Add(c);
    public override void Display() {
        Console.WriteLine($"🎯 {Name}");
        foreach (var c in _games) c.Display(); // recursivo
    }
}
```

---

## 4) Iterator + Composite — Sinergia en Gaming RTG
- **Necesidad:** Recorrer **toda la estructura** de juegos RTG (categorías y subcategorías) con código homogéneo.
- **Solución:** Cada `GameCategory` expone `CreateIterator()` → iterador que recorre toda la jerarquía.
- **Casos de uso:** Filtrar juegos con RTP > 97%, encontrar todos los juegos RTG de una serie, mostrar juegos promocionales.

**Ejemplo de filtro de RTP alto:**
```csharp
void ShowHighRTPGames(GameComponent rtgCasino) {
    var iterator = rtgCasino.CreateIterator();
    while (iterator.HasNext()) {
        var component = iterator.Next();
        try {
            if (component.RTP > 97.0m) {
                Console.WriteLine($"⭐ {component.Name} - {component.RTP}%");
            }
        } catch (NotSupportedException) {
            // Skip category headers
        }
    }
}
```

---

## 5) Anti‑patrones y trampas en Gaming RTG
- **Duplicar bucles** para cada serie de juegos RTG (Real Series vs i-Slots vs Progressive).
- **Exponer APIs internas** directamente (rompes encapsulación y abres puerta a cambios de API).
- **API Composite demasiado "gorda"**: obligar a juegos individuales a implementar métodos de categoría.
- **Orden de RTP** o **filtros* no definidos**: jugadores confundidos por resultados inconsistentes.

---

## 6) Checklist de diseño para Gaming RTG
- ¿El **cliente** conoce si los juegos RTG vienen de **base de datos, API REST, o archivos**? → Introduce Iterator.
- ¿Tienes **categorías anidadas** con operaciones uniformes (mostrar, filtrar, buscar)? → Usa Composite.
- ¿La API de `GameComponent` es **mínima** y **coherente**? (evitar métodos no aplicables a juegos individuales)
- ¿Definiste el **orden de RTP**, **filtros por serie RTG**, y **reglas de modificación** durante el recorrido?
- ¿Pruebas unitarias del **recorrido de catálogos** y del **comportamiento recursivo** de categorías?

---

## 7) Preguntas tipo examen (MCQ) - Contexto Casino RTG
1. ¿Cuál es el objetivo principal de Iterator en un casino RTG online?  
   A) Mejorar el RTP de los juegos RTG.  
   B) Recorrer catálogos de juegos RTG sin exponer las APIs internas.  
   C) Reemplazar todas las categorías por una única estructura.  
   **Respuesta:** B

2. En la jerarquía de juegos RTG, ¿qué clase contiene subcategorías y propaga operaciones?  
   A) GameComponent  
   B) CasinoGame  
   C) GameCategory  
   **Respuesta:** C

3. ¿Qué principio se refuerza al usar Iterator para múltiples series de juegos RTG?  
   A) LSP  
   B) DIP  
   C) OCP + Encapsulación  
   **Respuesta:** C

4. En Composite, ¿cómo maneja un juego RTG individual una operación como "agregar subcategoría"?  
   A) Implementación vacía silenciosa  
   B) Lanzar `NotSupportedException`  
   C) Convertir el juego en categoría  
   **Respuesta:** B

5. ¿Qué beneficio clave ofrece Composite en la gestión de juegos de casino RTG?  
   A) Mejor rendimiento de la base de datos  
   B) Tratamiento uniforme de juegos individuales y categorías completas  
   C) Reducción automática del house edge  
   **Respuesta:** B

---

## 8) Ejercicios prácticos - Contexto Gaming RTG

> 💡 **Tip:** Encuentra las soluciones completas en [CSharp_Practice_Solutions.cs](CSharp_Practice_Solutions.cs)

**A. Iterator para Catálogo RTG:** Tienes `Dictionary<string, CasinoGame>` donde la clave es el ID del juego RTG. Implementa un `IIterator<CasinoGame>` que recorra por RTP descendente.  
*Puntos clave:* ordenar por RTP, protegerte de modificaciones durante el recorrido, manejar juegos RTG con mismo RTP.

**B. Composite para Categorías RTG:** Modela categorías de casino RTG con `GameComponent` (base), `CasinoGame` (hoja), `GameCategory` (compuesto). Implementa `Display()` y `FindGamesByRTP(decimal minRTP)`.  
*Puntos clave:* recursividad, búsqueda por criterios de gaming, costo de operaciones en catálogos grandes de RTG.

**C. Iterator + Composite para Casino RTG Completo:** Jerarquía `RTGCasino` → `SlotsCategory` → `PromotionalGames`. Implementa iterador que recorra **todo** en preorden. Agrega filtro por proveedor "RTG".  
*Puntos clave:* navegación completa del árbol, filtros específicos de RTG gaming, rendimiento con miles de juegos.

---

## 9) Diferencias rápidas - Contexto Gaming RTG
| Tema               | Iterator                                   | Composite                                      |
|--------------------|--------------------------------------------|-----------------------------------------------|
| Enfoque Gaming     | Recorrido de catálogos RTG                 | Estructura de categorías jerárquicas          |
| Encapsula          | APIs internas RTG y estructura de datos    | Composición de juegos y tratamiento uniforme   |
| Beneficio clave    | Cliente no conoce origen de datos RTG      | Cliente no distingue Juego vs Categoría        |
| Riesgo Gaming      | Orden de RTP/filtros no definidos          | API inflada en juegos individuales            |
| Mejor juntos       | Iterar jerarquías de casino RTG de forma segura | Exponer `CreateIterator()` en categorías      |

---

## 10) Glosario Gaming RTG
- **RTP (Return to Player):** Porcentaje que el juego devuelve a los jugadores a largo plazo.
- **RTG (Real Time Gaming):** Proveedor de software de casino especializado en slots y juegos de mesa.
- **Real Series:** Serie de slots RTG con gráficos avanzados y características especiales.
- **i-Slots:** Slots interactivos RTG con historias y niveles de progresión.
- **Progressive Jackpot:** Premio acumulativo RTG que crece con cada apuesta.
- **Juego Promocional:** Juego RTG con bonificaciones especiales o eventos temporales.

---

## 11) Plan de estudio (2 sesiones de 45–60 min)
- **Sesión 1:** Problema de múltiples series RTG → Iterator (intención, diseño, práctica A con catálogos).  
- **Sesión 2:** Categorías de casino RTG → Composite → Sinergia con Iterator (prácticas B y C) → Repaso MCQ.

> **Consejo Gaming RTG:** Implementa primero versiones con pocos juegos RTG y series, luego añade validaciones (RTP, filtros, rendimiento) y pruebas con catálogos reales de miles de juegos.

---

## 12) Casos de uso reales en la industria RTG
- **Gestión de series RTG:** Organizar Real Series, i-Slots, Progressive Jackpots uniformemente.
- **Sistemas de recomendación:** Filtrar juegos RTG por preferencias del jugador.
- **Herramientas de análisis:** Reportes de RTP por categoría y serie RTG.
- **Gestión de promociones:** Organizar juegos RTG en campañas temporales.
- **APIs de casino:** Exponer catálogos RTG sin revelar estructura interna.

---

## 📚 **Recursos Complementarios para Profundizar**

### **Implementaciones Prácticas RTG:**
- **[💻 Soluciones de Práctica](CSharp_Practice_Solutions.cs)** - Código completo ejecutable con:
  - Iterator para catálogos de juegos RTG con ordenamiento por RTP
  - Composite para jerarquías complejas de casino RTG
  - Ejemplos combinados de ambos patrones trabajando juntos
  - Juegos RTG reales: Doragon's Gems, Whispers of Seasons, etc.

### **Experiencia Interactiva RTG:**
- **[📋 Demo Interactivo](IteratorCompositeDemo/INTERACTIVE_DEMO_GUIDE.md)** - Ejecuta `dotnet run` para:
  - Ver los patrones en acción con juegos RTG reales
  - Experiencia paso a paso con explicaciones detalladas
  - Perfecto para presentaciones y workshops

### **Verificación del Aprendizaje:**
- **Tests unitarios** - Ejecuta `dotnet test` para validar tu comprensión
- **19 pruebas exhaustivas** que cubren casos edge y escenarios reales RTG

---

**💡 Esta guía te proporciona la base teórica sólida para dominar Iterator y Composite en el contexto de gaming RTG. ¡Combínala con la práctica y el demo interactivo para una experiencia de aprendizaje completa con juegos RTG reales!** 🚀