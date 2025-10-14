# Guía de Estudio — Iterator & Composite (Head First Design Patterns)
**Capítulo:** *The Iterator and Composite Patterns: Well-Managed Collections*  
**Objetivo:** Dominar los conceptos, cuándo aplicarlos, sus beneficios, riesgos, y cómo combinarlos para gestionar colecciones y jerarquías sin acoplamiento.

---

## 1) Resumen ejecutivo
- **Problema:** El cliente termina sabiendo si la colección es `List`, `Array` o un árbol y duplica bucles/condiciones → rompe **OCP** y **encapsulación**.
- **Iterator:** Proporciona una forma **uniforme** de recorrer elementos **sin exponer** la representación interna.
- **Composite:** Permite modelar **jerarquías (árboles)** tratando **Hoja** y **Compuesto** de manera **uniforme**.
- **Juntos:** Iterator recorre el árbol Composite de manera segura; el cliente realiza operaciones (imprimir, filtrar, buscar) con código simple.

---

## 2) Iterator — Conceptos clave
**Intención:** “Proveer un modo estándar de recorrer una colección sin exponer su estructura interna.”  
**Participantes:**
- `Iterator<T>`: Define operaciones de recorrido (p. ej., `HasNext()`, `Next()`).
- `Aggregate<T>`: Define `CreateIterator()` para devolver un iterador.
- Concretos: `ConcreteIterator`, `ConcreteAggregate`.
**Beneficios:**
- Uniformidad y **ocultamiento** de estructura.
- Facilita **OCP**: nuevas colecciones solo requieren su iterador.
- Reutilizas y pruebas mejor la lógica de recorrido.
**Decisiones de diseño:**
- **Orden de recorrido:** por inserción, alfabético, personalizado…
- **Consistencia:** ¿qué pasa si cambian los elementos durante la iteración?
- **Estado interno:** cursores/índices; invalidación si hay cambios.

**Pseudocódigo C#:**
```csharp
public interface IIterator<T> {
    bool HasNext();
    T Next();
}

public interface IAggregate<T> {
    IIterator<T> CreateIterator();
}

// Cliente
void PrintAll(IIterator<MenuItem> it) {
    while (it.HasNext()) {
        var item = it.Next();
        Console.WriteLine($"{item.Name} - ${item.Price}");
    }
}
```

**Cuándo usarlo:**
- Tienes múltiples tipos de colecciones (lista, arreglo, mapa, etc.).
- Quieres exponer un recorrido **seguro** y desacoplado del almacenamiento.

---

## 3) Composite — Conceptos clave
**Intención:** “Componer objetos en **árboles** y permitir tratar **hojas** y **compuestos** de forma uniforme.”  
**Estructura:**
- `Component` (interfaz/base): operaciones comunes (p. ej., `Print()`, `Add`, `Remove`).
- `Leaf`: comportamiento atómico (p. ej., `MenuItem`).
- `Composite`: contiene `Component` hijos y **propaga** operaciones (recursividad).
**Beneficios:**
- Cliente simple: no distingue hoja/compuesto (polimorfismo).
- **Escalabilidad**: operaciones recursivas (imprimir, filtrar, buscar).
**Riesgos:**
- **API inflada:** métodos no aplicables en hojas → conviene lanzar `NotSupportedException` o separar interfaces.
- Controlar quién puede **modificar** la estructura (inmutabilidad vs edición).

**Pseudocódigo C#:**
```csharp
public abstract class MenuComponent {
    public virtual string Name => throw new NotSupportedException();
    public virtual decimal Price => throw new NotSupportedException();
    public virtual void Add(MenuComponent c) => throw new NotSupportedException();
    public virtual void Remove(MenuComponent c) => throw new NotSupportedException();
    public virtual void Print() => throw new NotSupportedException();
}

public class MenuItem : MenuComponent {
    private string _name; private decimal _price;
    public MenuItem(string name, decimal price) { _name = name; _price = price; }
    public override string Name => _name;
    public override decimal Price => _price;
    public override void Print() => Console.WriteLine($"{Name} - ${Price}");
}

public class Menu : MenuComponent {
    private List<MenuComponent> _children = new();
    private string _name;
    public Menu(string name) { _name = name; }
    public override string Name => _name;
    public override void Add(MenuComponent c) => _children.Add(c);
    public override void Print() {
        Console.WriteLine($"\n{Name}");
        foreach (var c in _children) c.Print(); // recursivo
    }
}
```

---

## 4) Iterator + Composite — Sinergia
- **Necesidad:** Recorrer **todo el árbol** (menús y submenús) con código homogéneo.
- **Solución:** Cada `Menu` expone `CreateIterator()` → un iterador externo/interno que recorre sus hijos. El cliente puede **filtrar** (ej. vegetarianos) sin conocer detalles.
- **Resultado:** Cliente **limpio**, estructura **encapsulada**, cambios locales y extensibilidad.

**Ejemplo de filtro:**
```csharp
void PrintVegetarian(MenuComponent root) {
    // Recorremos todo el árbol Composite
    // con un iterador que sabe navegar hijos.
    var it = root.CreateIterator(); // podría ser un iterador compuesto
    while (it.HasNext()) {
        var c = it.Next();
        if (c is MenuItem item && item.IsVegetarian) {
            item.Print();
        }
    }
}
```

---

## 5) Anti‑patrones y trampas
- **Duplicar bucles** en clientes para cada colección concreta.
- **Exponer colecciones internas** (rompes encapsulación y abres puerta a inconsistencias).
- **API Composite demasiado “gorda”**: obliga a hojas a implementar métodos que no tienen sentido.
- **Orden* o *consistencia* del iterador no definidos**: bugs sutiles.

---

## 6) Checklist de diseño
- ¿El **cliente** conoce la **estructura interna**? → Introduce Iterator.
- ¿Tienes **jerarquías** con operaciones uniformes? → Usa Composite.
- ¿La API de `Component` es **mínima** y **coherente**? (evitar métodos “no aplicables”)
- ¿Definiste el **orden** y las **reglas de modificación** durante el recorrido?
- ¿Pruebas unitarias del **recorrido** y del **comportamiento recursivo**?

---

## 7) Preguntas tipo examen (MCQ)
1. ¿Cuál es el objetivo principal de Iterator?  
   A) Mejorar el rendimiento de la colección.  
   B) Recorrer elementos sin exponer su representación interna.  
   C) Reemplazar colecciones heterogéneas por una única estructura.  
   **Respuesta:** B

2. En Composite, ¿qué clase contiene a los hijos y propaga operaciones?  
   A) Component  
   B) Leaf  
   C) Composite  
   **Respuesta:** C

3. ¿Qué principio se refuerza al usar Iterator?  
   A) LSP  
   B) DIP  
   C) OCP + Encapsulación  
   **Respuesta:** C

4. En Composite, ¿cómo maneja una operación no aplicable a una hoja?  
   A) Implementación vacía silenciosa  
   B) Lanzar `NotSupportedException` o separar interfaces  
   C) Convertir la hoja en compuesto  
   **Respuesta:** B

5. ¿Qué riesgo surge de “API inflada” en Composite?  
   A) Acoplamiento tiempo de ejecución  
   B) Hojas con métodos sin sentido → mayor complejidad y errores  
   C) No se puede iterar el árbol  
   **Respuesta:** B

---

## 8) Ejercicios prácticos (con guía)
**A. Iterator para Mapas:** Tienes `Dictionary<string, Product>`. Implementa un `IIterator<Product>` que recorra valores por orden de clave.  
*Puntos clave:* ordenar las claves, protegerte de modificaciones durante el recorrido, pruebas con diccionarios vacíos.

**B. Composite para UI:** Modela una UI con `View` (componente), `Label`/`Button` (hojas), `StackPanel`/`Grid` (compuestos). Implementa `Draw()` y `FindById()`.  
*Puntos clave:* recursividad, búsqueda en profundidad, costo de operaciones.

**C. Iterator + Composite para Sistema de Archivos:** `Folder` y `File`. Implementa un iterador que recorra **todo** el árbol en preorden. Agrega filtro por extensión `.log`.  
*Puntos clave:* pila/cola interna del iterador, evitar ciclos, pruebas con árboles grandes.

---

## 9) Diferencias rápidas
| Tema               | Iterator                                   | Composite                                      |
|--------------------|--------------------------------------------|-----------------------------------------------|
| Enfoque            | Recorrido                                   | Estructura jerárquica                          |
| Encapsula          | Detalles de la colección                    | Composición y tratamiento uniforme             |
| Beneficio clave    | Cliente no conoce (List/Array/Tree)         | Cliente no distingue Hoja vs Compuesto         |
| Riesgo             | Orden/consistencia no definidos             | API inflada / métodos no aplicables            |
| Mejor juntos       | Iterar árboles Composite de forma segura    | Exponer `CreateIterator()` para recorrer       |

---

## 10) Glosario mínimo
- **Recorrido externo vs interno:** El iterador “externo” lo controla el cliente; el “interno” lo controla la colección (ej. `ForEach`).
- **Hoja:** Nodo sin hijos.
- **Compuesto:** Nodo con hijos (contiene componentes).
- **OCP:** Open/Closed Principle: abierto a extensión, cerrado a modificación.

---

## 11) Plan de estudio (2 sesiones de 45–60 min)
- **Sesión 1:** Problema → Iterator (intención, diseño, práctica A).  
- **Sesión 2:** Composite → Sinergia con Iterator (prácticas B y C) → Repaso MCQ y checklist.

> Consejo: Implementa primero versiones mínimas funcionales y añade validaciones (orden, mutaciones) y pruebas después.
