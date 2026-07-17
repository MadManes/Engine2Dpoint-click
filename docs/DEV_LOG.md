# Engine 2D Point & Click - Registro de desarrollo

## Etapa 1 - Arquitectura inicial

Se decidió desarrollar el engine en C# debido a que permite una arquitectura modular fuerte mediante proyectos separados, interfaces y control estricto de dependencias.

El objetivo es crear un motor para aventuras gráficas 2D tipo point & click, con sistemas desacoplados y extensibles.

---

# Etapa 2 - Creación de Solution y módulos

## Solution (.sln)

Una Solution (`.sln`) es un contenedor que agrupa múltiples proyectos C# (`.csproj`).

Permite dividir el engine en módulos independientes, cada uno con responsabilidades claras.

A diferencia de Python, donde normalmente un proyecto está formado por paquetes y módulos dentro de una misma estructura, C# permite separar partes del sistema en proyectos compilables independientes.

---

## Proyectos creados

Dentro de la carpeta `src`:

- Engine.Core
- Engine.Entities
- Engine.Navigation
- Engine.Scene
- Engine.Runtime

Cada módulo posee su propio archivo `.csproj`.

---

# Dependencias entre módulos

Las dependencias se declaran mediante referencias entre proyectos.

Un proyecto puede utilizar solamente los proyectos que tiene referenciados.


- Relaciones actuales:
Entities depende de Core
Scene depende de Core y Entities
Navigation depende de Core, Entities y Scene
Runtime depende de Navigation, Core, Entities y Scene


---

# Arquitectura actual
Engine.Runtime
    |
    |
    +---- Engine.Navigation
    |
    +---- Engine.Scene
    |
    +---- Engine.Entities
    |
    +---- Engine.Core


---

# Conceptos nuevos de C#

## Proyecto (.csproj)

Archivo que define un proyecto C#.

Contiene:
- configuración de compilación
- referencias a otros proyectos
- dependencias


## Solution (.sln)

Agrupa varios proyectos relacionados.


## Class Library

Tipo de proyecto utilizado para crear módulos reutilizables.

Ejemplo:

Engine.Core
Engine.Navigation
Engine.Entities

No poseen un punto de entrada `Main()`.


## Console Application

Proyecto ejecutable.

En este engine:

Engine.Runtime

Será el punto inicial que ejecutará el motor mediante `Program.cs`.

---

# Estado actual

La solución compila correctamente.

La arquitectura base está creada.

Todavía no se agregaron clases del engine.

Próximo paso:
Crear las clases iniciales comenzando por Engine.Core.

------------------------------------------

# Etapa 3 - Núcleo inicial del engine

## Engine.Core

Se implementó el ciclo de vida básico del motor mediante:

- `IGameLoop`: interfaz que define el contrato del ciclo principal.
- `Time`: calcula el tiempo transcurrido entre frames (`deltaTime`).
- `GameEngine`: controla la ejecución del motor (`Initialize → Update → Render → Shutdown`).

Se utilizó **inyección de dependencias**, permitiendo que el motor reciba cualquier implementación de `IGameLoop` sin conocer el juego concreto.

---

## Engine.Runtime

Se creó el proyecto ejecutable del motor.

Contiene:

- `Program.cs`: punto de entrada de la aplicación.
- `Game`: implementación temporal de `IGameLoop` utilizada para probar el funcionamiento del motor.

Se verificó correctamente el ciclo de ejecución completo del engine.

---

## Engine.Math

Se creó un módulo independiente para centralizar todas las estructuras matemáticas del motor.

Primera implementación:

- `Vector2`

Se decidió crear un `Vector2` propio para mantener el engine desacoplado de bibliotecas gráficas como Raylib.

`Vector2` fue implementado como `struct`, ya que representa un tipo de valor pequeño e inmutable conceptualmente.

---

## Engine.Entities

Se modificó `Transform`.

Antes:

- X
- Y

Ahora:

- Position (`Vector2`)
- Z
- Scale

La posición del mundo deja de representarse mediante coordenadas independientes y pasa a utilizar un vector bidimensional.

Esta decisión simplificará el desarrollo del sistema de navegación y del renderizado.

---

## Arquitectura actual

Engine.Runtime
│
├── Engine.Navigation
├── Engine.Scene
├── Engine.Entities
├── Engine.Math
└── Engine.Core

---

## Estado del proyecto

- Arquitectura modular creada.
- Ciclo principal del engine funcionando.
- Primer ejecutable del motor operativo.
- Sistema de entidades iniciado.
- Primer módulo matemático implementado.
- Transform preparado para futuras operaciones de navegación.

-------------------------------------------------------------------------------------
# Etapa 4 - Scene y representación del mundo

## Objetivo

Se comenzó la implementación del modelo del mundo del engine.

Se tomó la decisión arquitectónica de separar el concepto de **Scene** del concepto de **World**.

Una escena representa el estado completo de una habitación del juego, mientras que el mundo contiene la información física necesaria para recorrerla.

Esta separación permitirá soportar distintos tipos de escenarios sin modificar el sistema de navegación.

---

## Engine.Scene

Se implementaron:

- `IScene`
- `Scene`
- `SceneManager`

### Responsabilidades

**Scene**

- Mantiene la colección de entidades existentes.
- Posee un nombre.
- Contiene un objeto `World`.

**SceneManager**

- Mantiene la escena actualmente activa.
- Será el responsable futuro de cargar y cambiar escenas.

---

## World

Se creó la clase:

- `World`

Actualmente contiene:

- `WalkableArea`

En el futuro contendrá además:

- mapas de profundidad
- obstáculos
- zonas de sonido
- luces
- clima
- scripts
- datos de navegación
- cámaras

---

## WalkableArea

Se introdujo una nueva abstracción:

- `IWalkableArea`

Responsabilidad:

Determinar si una posición pertenece o no a una zona caminable.

Todavía no calcula caminos.

Su única responsabilidad es representar las zonas por las cuales una entidad puede desplazarse.

---

## NavigationType

Se creó el enum:

- Grid
- Polygon
- Path
- Hybrid

Este enum permitirá que cada habitación indique qué sistema de navegación utilizará.

La decisión del algoritmo deja de pertenecer al personaje y pasa a pertenecer al mundo.

---

## Decisión arquitectónica importante

El motor no estará diseñado alrededor del personaje.

Estará diseñado alrededor del mundo.

La navegación siempre preguntará primero cómo es la habitación y luego decidirá qué algoritmo utilizar.

Esto permitirá implementar distintos motores de navegación sin modificar el resto del engine.

---

## Estado actual del engine

Engine.Runtime
│
├── inicia el motor
│
▼

GameEngine
│
├── controla el ciclo principal
│
▼

SceneManager
│
▼

Scene
│
├── Entities
└── World
      │
      └── WalkableArea

-- HASTA ACA --
# DEV LOG

## Etapa 1 - Arquitectura base

- Creación de la solución (.sln).
- Separación del engine en módulos independientes.
- Configuración de referencias entre proyectos.
- Limpieza de la estructura generada por las plantillas de .NET.

Dependencias actuales:

Engine.Core
↓

Engine.Math

↓

Engine.Entities

↓

Engine.Scene

↓

Engine.Navigation

↓

Engine.Runtime

---

## Etapa 2 - Runtime

Se implementó el ciclo principal del engine:

- IGame
- IGameLoop
- Engine
- Primer GameLoop de prueba

Objetivo:

Establecer la estructura principal del motor antes de agregar renderizado y lógica.

---

## Etapa 3 - Sistema matemático (Engine.Math)

Se creó el primer módulo reutilizable del engine.

### Vector2

Implementado:

- Suma
- Resta
- Multiplicación por escalar
- División por escalar
- Magnitud (Length)
- Normalización
- Producto escalar (Dot)
- Distancia entre vectores (Distance)

Conceptos aprendidos:

- Sobrecarga de operadores.
- Métodos de instancia.
- Encapsulación de operaciones matemáticas.

---

### MathHelper

Se creó una clase estática para centralizar utilidades matemáticas del engine.

Actualmente implementa:

- Clamp()

Objetivo:

Evitar depender directamente de utilidades dispersas y mantener una API matemática propia del motor.

---

### LineSegment

Nueva abstracción geométrica.

Representa un segmento entre dos puntos.

Implementado:

- Length()
- Direction()
- MidPoint()
- ClosestPoint()

Conceptos aprendidos:

- Proyección de un punto sobre un segmento.
- Uso del producto escalar.
- Clamping del parámetro t.
- Validación de segmentos degenerados.

---

### Polygon

Representación de polígonos arbitrarios.

Implementado:

- Containment mediante Ray Casting.
- Construcción automática de bordes (Edges).
- GetClosestPoint()

Conceptos aprendidos:

- Ray Casting.
- Recorrido de vértices.
- Uso del operador módulo (%) para cerrar polígonos.
- Delegación de responsabilidades hacia LineSegment.

---

## Etapa 4 - Mundo y navegación

Se comenzó la separación entre geometría y navegación.

### IWalkableArea

Define la interfaz para cualquier región caminable.

Implementa:

- Contains()
- GetClosestPoint()
- NavigationType

---

### WalkableArea

Representa una región caminable individual.

Delega la geometría al Polygon.

---

### NavigationArea

Nueva clase que representa la navegación completa de una habitación.

Implementa:

- Colección de múltiples WalkableArea.
- Contains()
- GetClosestPoint()

Conceptos aprendidos:

- IReadOnlyList
- foreach
- Delegación entre capas
- Principio de Responsabilidad Única (SRP)

---

### Arquitectura alcanzada

World

↓

NavigationArea

↓

WalkableArea

↓

Polygon

↓

LineSegment

↓

Vector2

Cada nivel conoce únicamente la capa inmediatamente inferior.