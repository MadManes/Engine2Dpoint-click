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


