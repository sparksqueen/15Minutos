# 15 Minutos - Documentación del Juego

## 📋 Descripción General

**15 Minutos** es un juego de limpieza y organización con límite de tiempo. El jugador tiene 15 minutos (8 minutos reales) para ordenar y limpiar objetos en diferentes niveles, mientras gestiona el "caosómetro" que mide el desorden restante.

## 🎮 Mecánicas Principales

### Sistema de Tiempo
- **Tiempo visible**: 15 minutos (mostrado en pantalla)
- **Tiempo real**: 8 minutos de juego efectivo
- El reloj cuenta regresivamente desde 15:00 hasta 00:00
- Eventos especiales se activan a los 10 minutos y 5 minutos restantes

### Sistema de Caosómetro
- Mide el desorden restante en el nivel
- Se inicializa contando todos los objetos con tags "Interactable" y "Pickup"
- Disminuye cuando se ordenan objetos
- Al llegar a 0, se muestra el final perfecto

### Sistema de Niveles
El juego tiene 3 niveles:
1. **Nivel 1 (Tutorial - Abuela)**: Nivel introductorio
2. **Nivel 2 (Original)**: Nivel principal
3. **Nivel 3 (Museo)**: Nivel avanzado

## 🎯 Controles

- **Flechas / WASD**: Movimiento del jugador
- **Espacio**: Interactuar con objetos (limpiar, eliminar)
- **G (mantener)**: Agarrar y mover objetos con tag "Pickup"
- **M**: Ver el mapa (según configuración)

## 📁 Estructura del Proyecto

```
Assets/
├── Scripts/          # Scripts principales del juego
├── Scenes/           # Escenas del juego
├── Prefabs/          # Prefabs reutilizables
├── Sprites/          # Sprites y texturas
├── Animation/        # Animaciones y controladores
├── Audio/            # Sonidos y música
└── UI/               # Elementos de interfaz
```

## 🔧 Scripts Principales

### GameManager.cs
**Responsabilidad**: Gestiona el estado general del juego.

**Funcionalidades**:
- Controla si el juego está activo (`gameActive`)
- Inicia el juego (`StartGame()`)
- Finaliza el juego (`EndGame()`)
- Singleton pattern para acceso global

**Uso**:
```csharp
GameManager.Instance.StartGame();
GameManager.Instance.EndGame();
```

---

### LevelManager.cs
**Responsabilidad**: Gestiona la carga y activación de niveles.

**Configuración requerida**:
- `lvl1Obj`, `lvl2Obj`, `lvl3Obj`: Objetos padre de cada nivel
- `lvl1Limits`, `lvl2Limits`, `lvl3Limits`: Límites de cada nivel
- `spawnLvl1`, `spawnLvl2`, `spawnLvl3`: Puntos de spawn del jugador
- `player`: Referencia al jugador
- `caosometroManager`: Referencia al manager del caosómetro

**Funcionalidades**:
- `InicializarNivel(int nivel)`: Activa el nivel especificado
- `GetCurrentLevel()`: Retorna el nivel actual
- Bloquea el movimiento del jugador durante 33 segundos al iniciar (intro)

**Uso**:
```csharp
LevelManager.Instance.InicializarNivel(1); // Inicia nivel 1
```

---

### UIManager.cs
**Responsabilidad**: Gestiona todas las pantallas de UI del juego.

**Configuración requerida**:
- `ID_Nivel_Actual`: ID del nivel actual (1, 2, o 3)
- Referencias a todas las pantallas de UI (título, selección de nivel, game over, etc.)

**Funcionalidades**:
- `OnLevel1Selected()`, `OnLevel2Selected()`, `OnLevel3Selected()`: Inicializa el nivel seleccionado
- `ShowGameOver(string finalText)`: Muestra pantalla de fin de juego
- `ShowFinalPerfecto()`: Muestra pantalla de victoria perfecta
- `OcultarTodasLasUI()`: Oculta todas las pantallas

**Pantallas de UI**:
- `titleScreen`: Pantalla de título
- `levelSelectScreen`: Selección de niveles
- `mainUI`: UI principal del juego
- `caosometroUI`: UI del caosómetro
- `gameOverNivel1/2/3`: Pantallas de game over por nivel
- `finalPerfectoNivel1/2/3`: Pantallas de victoria por nivel

---

### TimerManager.cs
**Responsabilidad**: Gestiona el temporizador del juego.

**Configuración**:
- `duracionReal`: 480 segundos (8 minutos reales)
- `duracionVisible`: 900 segundos (15 minutos visibles)

**Funcionalidades**:
- Cuenta regresiva visual de 15:00 a 00:00
- Activa eventos en 10:00 y 5:00 minutos restantes
- Finaliza el juego al llegar a 0

**Eventos**:
- **Evento 1**: Se activa a los 10 minutos restantes
- **Evento 2**: Se activa a los 5 minutos restantes

---

### CaosometroManager.cs
**Responsabilidad**: Gestiona el sistema de caosómetro.

**Configuración requerida**:
- `caosometroSlider`: Slider de UI que muestra el caos

**Funcionalidades**:
- `Inicializar()`: Cuenta objetos con tags "Interactable" y "Pickup"
- `ObjetoOrdenado()`: Reduce el caos cuando se ordena un objeto
- `ReducirCaos()`: Disminuye el contador de objetos restantes
- Muestra "Final Perfecto" cuando todos los objetos están ordenados

**Tags requeridos**:
- `Interactable`: Objetos que se pueden limpiar/interactuar
- `Pickup`: Objetos que se pueden agarrar y mover

---

### Movement.cs
**Responsabilidad**: Controla el movimiento del jugador.

**Configuración**:
- `speed`: Velocidad de movimiento (default: 2.5)

**Funcionalidades**:
- Movimiento con flechas/WASD
- Animaciones basadas en dirección
- Control de movimiento habilitado/deshabilitado

**Métodos públicos**:
- `SetMovimientoHabilitado(bool activo)`: Activa/desactiva el movimiento

---

### PlayerPickup.cs
**Responsabilidad**: Sistema de agarrar y soltar objetos.

**Configuración requerida**:
- `holdPoint`: Transform donde se posiciona el objeto agarrado

**Funcionalidades**:
- Detecta objetos con tag "Pickup" cerca del jugador
- Agarra objetos al mantener **G**
- Suelta objetos al soltar **G**
- Detecta si el objeto se suelta sobre una zona objetivo
- Organiza objetos en el contenedor del nivel correspondiente

**Lógica de suelta**:
- Si se suelta sobre una zona (tag "Zona" o "ZonaObjetivo"): El objeto queda libre para que `ZonaObjetivo` lo posicione
- Si se suelta en el suelo: El objeto se guarda en el contenedor del nivel actual

---

### InteractableObject.cs
**Responsabilidad**: Objetos con los que el jugador puede interactuar.

**Tipos de interacción**:
- `CambiarSprite`: Cambia el sprite del objeto (limpieza)
- `Desactivar`: Desactiva el objeto
- `Destruir`: Destruye el objeto
- `PegarAlJugador`: Muestra prompt pero no hace nada (para objetos que se agarran)

**Configuración**:
- `objetoID`: Identificador único del objeto
- `tipoDeInteraccion`: Tipo de interacción
- `spriteLimpio`: Sprite a mostrar después de limpiar
- `brushSoundClip`: Sonido de limpieza (opcional)

**Funcionalidades**:
- Muestra prompt cuando el jugador está cerca
- Reproduce sonido de limpieza al interactuar
- Notifica al `CaosometroManager` cuando se ordena

---

### ZonaObjetivo.cs
**Responsabilidad**: Zonas donde se deben colocar objetos específicos.

**Configuración requerida**:
- `posicionFinal`: Transform del punto de encaje (objeto vacío "PuntoDeEncaje")
- `puntoReferencia`: Qué parte del sprite debe coincidir con el punto de encaje

**Opciones de PuntoReferencia**:
- `PivoteDelSprite`: Usa el pivote del sprite (default)
- `Centro`: Centro del sprite
- `CentroInferior`: Centro de la parte inferior
- `EsquinaInferiorIzquierda`: Esquina inferior izquierda
- `EsquinaInferiorDerecha`: Esquina inferior derecha
- `EsquinaSuperiorIzquierda`: Esquina superior izquierda
- `EsquinaSuperiorDerecha`: Esquina superior derecha
- `CentroSuperior`: Centro de la parte superior
- `CentroIzquierda`: Centro del lado izquierdo
- `CentroDerecha`: Centro del lado derecho

**Funcionalidades**:
- Detecta cuando un objeto entra en la zona
- Compara el nombre del objeto con el nombre de la zona
- Posiciona el objeto en el `posicionFinal` ajustando por el punto de pivote
- Desactiva físicas y collider del objeto
- Cambia el tag del objeto a "Untagged"
- Agrega componente `YaEntregado` para evitar duplicados

**Importante**: El nombre del objeto debe coincidir exactamente con el nombre de la zona.

---

### TaskManager.cs
**Responsabilidad**: Gestiona el sistema de tareas (actualmente básico).

**Funcionalidades**:
- `RegisterTaskCompletion(string taskName)`: Registra una tarea completada
- `GetCompletionPercent()`: Retorna el porcentaje de completitud

**Configuración**:
- `allTasks`: Lista de nombres de tareas

---

### Task.cs
**Responsabilidad**: Componente individual de tarea.

**Funcionalidades**:
- `Interact()`: Marca la tarea como completada
- Se desactiva al completarse

---

### FinalEvaluator.cs
**Responsabilidad**: Evalúa el resultado final del juego.

**Funcionalidades**:
- `Evaluate()`: Evalúa el porcentaje de completitud
- Muestra diferentes finales según el porcentaje:
  - 100%: "Final Perfecto"
  - ≥50%: "Final Meh"
  - <50%: "Final Catastrófico"

---

### EventTrigger.cs
**Responsabilidad**: Gestiona eventos especiales durante el juego.

**Eventos**:
- **Evento 1**: Llamado del ex (a los 10 minutos)
- **Evento 2**: Vecina chusma (a los 5 minutos)

**Funcionalidades**:
- `TriggerEvent(int eventNumber)`: Activa un evento específico

---

### IntroAnimacionManager.cs
**Responsabilidad**: Gestiona la animación introductoria de cada nivel.

**Configuración**:
- `tiempoDuracion`: Duración de la intro en segundos (default: 4.0)
- `panelVisual`: Panel visual opcional a mostrar

**Funcionalidades**:
- Bloquea el movimiento del jugador durante la intro
- Muestra panel visual si está configurado
- Libera al jugador después del tiempo especificado

---

### SoundManager.cs
**Responsabilidad**: Gestiona los sonidos del juego.

**Funcionalidades**:
- Reproduce sonidos de limpieza
- Singleton para acceso global

---

### MusicController.cs
**Responsabilidad**: Gestiona la música de fondo.

**Funcionalidades**:
- Reproduce música de fondo
- Singleton para acceso global

---

### VecinoController.cs
**Responsabilidad**: Controla el comportamiento de los vecinos NPCs.

**Funcionalidades**:
- Movimiento hacia objetivos
- Sistema de diálogos
- Desordenar objetos (según configuración)

---

### MapManager.cs
**Responsabilidad**: Gestiona el mapa del nivel.

**Funcionalidades**:
- Muestra/oculta el mapa
- Navegación por el mapa

---

### CameraFollow.cs
**Responsabilidad**: Hace que la cámara siga al jugador.

**Funcionalidades**:
- Seguimiento suave del jugador
- Ajustes de offset y límites

---

## 🏗️ Configuración de Niveles

### Configurar un Nuevo Nivel

1. **Crear estructura de objetos**:
   ```
   NivelX
   ├── Objetos (padre de todos los objetos del nivel)
   ├── Límites (colliders que definen los bordes)
   └── SpawnPoint (punto de spawn del jugador)
   ```

2. **Configurar LevelManager**:
   - Asignar `lvlXObj` al objeto padre de objetos
   - Asignar `lvlXLimits` al objeto de límites
   - Asignar `spawnLvlX` al punto de spawn

3. **Configurar UIManager**:
   - Asignar `gameOverNivelX` y `finalPerfectoNivelX`
   - Configurar `ID_Nivel_Actual` si es necesario

4. **Crear zonas objetivo**:
   - Crear objetos con componente `ZonaObjetivo`
   - Nombrar la zona igual que el objeto que debe ir ahí
   - Crear objeto vacío "PuntoDeEncaje" y asignarlo a `posicionFinal`
   - Configurar `puntoReferencia` según necesidad

---

## 🎨 Configuración de Objetos

### Objetos Interactables (Limpieza)

1. Agregar componente `InteractableObject`
2. Configurar:
   - `tipoDeInteraccion`: `CambiarSprite`
   - `spriteLimpio`: Sprite limpio del objeto
   - `brushSoundClip`: Sonido de limpieza (opcional)
3. Agregar tag: `Interactable`
4. Agregar `Collider2D` con `Is Trigger = true`

### Objetos Pickup (Agarrables)

1. Agregar tag: `Pickup`
2. Agregar `Collider2D` (puede ser trigger o no)
3. Agregar `Rigidbody2D` si se quiere física
4. El nombre del objeto debe coincidir con el nombre de la zona objetivo

### Zonas Objetivo

1. Crear objeto vacío para la zona
2. Agregar componente `ZonaObjetivo`
3. Nombrar la zona igual que el objeto que debe ir ahí
4. Crear objeto vacío "PuntoDeEncaje" en la posición deseada
5. Asignar "PuntoDeEncaje" a `posicionFinal`
6. Configurar `puntoReferencia` según necesidad
7. Agregar `Collider2D` con `Is Trigger = true`
8. Agregar tag: `Zona` o `ZonaObjetivo`

---

## 🔧 Ajuste de Puntos de Pivote

Si los objetos no se posicionan correctamente en las zonas:

1. **Desde Unity (recomendado)**:
   - Selecciona el sprite en el Project
   - En el Inspector, haz clic en "Sprite Editor"
   - Ajusta el Pivot en la parte superior
   - Aplica los cambios

2. **Desde el script ZonaObjetivo**:
   - Selecciona la zona objetivo
   - En el Inspector, ajusta `Punto Referencia`
   - Prueba diferentes opciones:
     - Si queda por arriba: `Centro Inferior` o `Esquina Inferior Izquierda`
     - Si queda por abajo: `Centro Superior` o `Esquina Superior Izquierda`

---

## 🐛 Solución de Problemas Comunes

### Los objetos no se posicionan correctamente
- Verifica que el nombre del objeto coincida exactamente con el nombre de la zona
- Ajusta el `puntoReferencia` en `ZonaObjetivo`
- Verifica que el `PuntoDeEncaje` esté en la posición correcta
- Revisa los puntos de pivote de los sprites

### El caosómetro no se actualiza
- Verifica que los objetos tengan los tags correctos: `Interactable` o `Pickup`
- Asegúrate de que `CaosometroManager` tenga asignado el slider
- Revisa la consola para ver si hay errores

### El jugador no se mueve
- Verifica que `Movement.SetMovimientoHabilitado(true)` esté siendo llamado
- Revisa que el jugador tenga el tag "Player"
- Verifica que no esté en la intro (33 segundos de bloqueo)

### Los objetos no se pueden agarrar
- Verifica que tengan el tag "Pickup"
- Asegúrate de que `PlayerPickup.holdPoint` esté asignado
- Verifica que el jugador tenga un `Collider2D`

### El timer no funciona
- Verifica que `TimerManager.Instance` exista
- Asegúrate de que `GameManager.Instance.gameActive` sea `true`
- Revisa que `timerText` esté asignado en el Inspector

---

## 📝 Notas de Desarrollo

### Singleton Pattern
Muchos scripts usan el patrón Singleton para acceso global:
- `GameManager.Instance`
- `LevelManager.Instance`
- `UIManager.Instance`
- `TimerManager.Instance`
- `TaskManager.Instance`
- `FinalEvaluator.Instance`
- `EventTrigger.Instance`
- `SoundManager.Instance`
- `MusicController.Instance`

### Tags Importantes
- `Player`: Jugador
- `Pickup`: Objetos agarrables
- `Interactable`: Objetos interactuables
- `Zona` / `ZonaObjetivo`: Zonas donde colocar objetos
- `Prompt`: Objeto de texto de prompt

### Orden de Inicialización
1. `UIManager` se inicializa y carga el nivel
2. `LevelManager` activa el nivel y bloquea al jugador
3. `GameManager` inicia el juego después de 33 segundos
4. `TimerManager` comienza a contar
5. `CaosometroManager` cuenta los objetos

---

## 🎵 Audio

### Sonidos
- Sonido de limpieza: `Assets/Audio/brush-83215.mp3`
- Se puede asignar en `InteractableObject.brushSoundClip`

### Música
- Se gestiona a través de `MusicController`
- Se carga desde `Resources/Prefabs/MusicManager`

---

## 📚 Recursos Adicionales

- **Sprites**: `Assets/Sprites/`
- **Animaciones**: `Assets/Animation/`
- **Prefabs**: `Assets/Prefabs/` y `Assets/Resources/Prefabs/`
- **Escenas**: `Assets/Scenes/`

---

## 🔄 Flujo del Juego

1. **Pantalla de Título**: Usuario presiona "Play"
2. **Selección de Nivel**: Usuario elige nivel 1, 2 o 3
3. **Inicialización**:
   - Se activa el nivel seleccionado
   - Se bloquea el movimiento del jugador (33 segundos)
   - Se reproduce la intro
4. **Juego**:
   - Jugador puede moverse
   - Timer comienza a contar
   - Caosómetro se inicializa
   - Jugador ordena objetos
5. **Finalización**:
   - Si se acaba el tiempo: Game Over
   - Si se ordenan todos los objetos: Final Perfecto
   - `FinalEvaluator` evalúa el resultado

---

## 📞 Soporte

Para problemas o preguntas sobre el código, revisa:
- Los comentarios en los scripts
- Los logs de Debug en la consola de Unity
- Esta documentación
- IG: @sparksqueen


---

**Última actualización**: 2025

