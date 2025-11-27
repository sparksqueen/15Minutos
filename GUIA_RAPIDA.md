# Guía Rápida de Referencia - 15 Minutos

## 🚀 Inicio Rápido

### Configurar un Nuevo Nivel

```
1. Crear estructura:
   - NivelX (GameObject padre)
     - Objetos (padre de objetos)
     - Límites (colliders)
     - SpawnPoint (Transform)

2. En LevelManager:
   - lvlXObj → Objetos
   - lvlXLimits → Límites  
   - spawnLvlX → SpawnPoint

3. En UIManager:
   - gameOverNivelX → Pantalla Game Over
   - finalPerfectoNivelX → Pantalla Victoria
```

### Crear un Objeto Interactable

```
1. GameObject con:
   - InteractableObject component
   - Collider2D (Is Trigger = true)
   - Tag: "Interactable"

2. Configurar:
   - tipoDeInteraccion: CambiarSprite
   - spriteLimpio: Sprite limpio
```

### Crear un Objeto Pickup

```
1. GameObject con:
   - Tag: "Pickup"
   - Collider2D
   - Rigidbody2D (opcional)
   - Nombre = Nombre de la zona objetivo

2. El objeto se puede agarrar con G
```

### Crear una Zona Objetivo

```
1. GameObject vacío:
   - Nombre = Nombre del objeto que debe ir ahí
   - ZonaObjetivo component
   - Collider2D (Is Trigger = true)
   - Tag: "Zona" o "ZonaObjetivo"

2. Crear "PuntoDeEncaje" (objeto vacío):
   - Posición donde debe ir el objeto
   - Asignar a posicionFinal en ZonaObjetivo

3. Configurar puntoReferencia según necesidad
```

---

## 🎮 Controles

| Tecla | Acción |
|-------|--------|
| Flechas / WASD | Mover jugador |
| Espacio | Interactuar (limpiar) |
| G (mantener) | Agarrar objeto |
| M | Ver mapa |

---

## 🏷️ Tags Importantes

| Tag | Uso |
|-----|-----|
| `Player` | Jugador |
| `Pickup` | Objetos agarrables |
| `Interactable` | Objetos interactuables |
| `Zona` / `ZonaObjetivo` | Zonas objetivo |
| `Prompt` | Texto de prompt UI |

---

## 📊 Sistema de Tiempo

```
Tiempo Visible: 15:00 → 00:00
Tiempo Real: 8 minutos

Eventos:
- 10:00 → Evento 1 (Llamado del ex)
- 5:00 → Evento 2 (Vecina chusma)
- 0:00 → Game Over
```

---

## 🔧 Scripts Singleton

```csharp
GameManager.Instance
LevelManager.Instance
UIManager.Instance
TimerManager.Instance
TaskManager.Instance
FinalEvaluator.Instance
EventTrigger.Instance
SoundManager.Instance
MusicController.Instance
```

---

## 🎯 Flujo de Objetos Pickup

```
1. Jugador presiona G cerca de objeto con tag "Pickup"
2. PlayerPickup.TryPickup() detecta el objeto
3. Objeto se hace hijo de holdPoint
4. Jugador suelta G
5. PlayerPickup.DropObject() suelta el objeto
6. Si está sobre zona → ZonaObjetivo lo posiciona
7. Si no → Se guarda en contenedor del nivel
```

---

## 🧹 Flujo de Objetos Interactables

```
1. Jugador se acerca a objeto con tag "Interactable"
2. InteractableObject muestra prompt
3. Jugador presiona Espacio
4. InteractableObject.EjecutarInteraccion()
5. Reproduce sonido de limpieza
6. Cambia sprite / desactiva / destruye
7. Notifica a CaosometroManager
8. Cambia tag a "Untagged"
```

---

## 📍 Sistema de Posicionamiento

### Problema: Objeto queda por arriba/abajo

**Solución 1**: Ajustar `puntoReferencia` en `ZonaObjetivo`
- Por arriba → `CentroInferior` o `EsquinaInferiorIzquierda`
- Por abajo → `CentroSuperior` o `EsquinaSuperiorIzquierda`

**Solución 2**: Ajustar pivote del sprite
- Seleccionar sprite → Sprite Editor → Ajustar Pivot

---

## 🐛 Debug Común

### Objeto no se posiciona
```csharp
// Verificar en consola:
Debug.Log("Objeto: " + nombreObjeto);
Debug.Log("Zona: " + nombreZona);
// Deben ser iguales exactamente
```

### Caosómetro no cuenta
```csharp
// Verificar tags:
- Objeto debe tener tag "Interactable" o "Pickup"
- Objeto debe estar activo (activeInHierarchy)
```

### Jugador no se mueve
```csharp
// Verificar:
Movement movimiento = player.GetComponent<Movement>();
movimiento.SetMovimientoHabilitado(true);
```

---

## 📝 Configuración de UIManager

```csharp
ID_Nivel_Actual = 1, 2, o 3  // Nivel actual

// Pantallas por nivel:
gameOverNivel1/2/3
finalPerfectoNivel1/2/3
```

---

## 🎨 Ajuste de Puntos de Pivote

### Valores Normalizados (0-1)

| Punto | Valor |
|-------|-------|
| Esquina Inferior Izquierda | (0, 0) |
| Centro | (0.5, 0.5) |
| Esquina Superior Derecha | (1, 1) |
| Centro Inferior | (0.5, 0) |
| Centro Superior | (0.5, 1) |

---

## 🔄 Orden de Inicialización

```
1. UIManager.Awake()
2. LevelManager.InicializarNivel()
3. Jugador bloqueado (33 segundos)
4. GameManager.StartGame()
5. TimerManager.StartTimer()
6. CaosometroManager.Inicializar()
7. Jugador liberado
```

---

## 📦 Estructura de Prefabs

```
Resources/Prefabs/
├── MusicManager
└── SoundManager
```

---

## 🎵 Audio

```csharp
// Sonido de limpieza:
InteractableObject.brushSoundClip
// O desde Resources:
Resources.Load<AudioClip>("Audio/brush-83215")
```

---

## ⚡ Métodos Públicos Importantes

### LevelManager
```csharp
InicializarNivel(int nivel)
GetCurrentLevel() → int
```

### UIManager
```csharp
OnLevel1Selected()
OnLevel2Selected()
OnLevel3Selected()
ShowGameOver(string finalText)
ShowFinalPerfecto()
```

### Movement
```csharp
SetMovimientoHabilitado(bool activo)
```

### PlayerPickup
```csharp
IsHolding(GameObject obj) → bool
ForzarSoltar()
```

### CaosometroManager
```csharp
Inicializar()
ObjetoOrdenado()
ReducirCaos()
```

### TimerManager
```csharp
StartTimer()
```

---

## 🎯 Checklist de Configuración

### Para un Nuevo Nivel
- [ ] Estructura de objetos creada
- [ ] LevelManager configurado
- [ ] UIManager con pantallas asignadas
- [ ] Spawn point configurado
- [ ] Límites del nivel configurados
- [ ] Objetos con tags correctos
- [ ] Zonas objetivo configuradas
- [ ] Puntos de encaje posicionados

### Para un Objeto Pickup
- [ ] Tag "Pickup" asignado
- [ ] Nombre = Nombre de zona objetivo
- [ ] Collider2D configurado
- [ ] Rigidbody2D (opcional)

### Para una Zona Objetivo
- [ ] Nombre = Nombre del objeto
- [ ] ZonaObjetivo component
- [ ] PuntoDeEncaje creado y asignado
- [ ] puntoReferencia configurado
- [ ] Collider2D (Is Trigger = true)
- [ ] Tag "Zona" o "ZonaObjetivo"

---

## 🔍 Búsqueda Rápida de Problemas

| Problema | Solución |
|----------|----------|
| Objeto no se posiciona | Verificar nombres coinciden |
| Objeto queda mal posicionado | Ajustar puntoReferencia |
| Caosómetro no cuenta | Verificar tags y objetos activos |
| Jugador no se mueve | Verificar SetMovimientoHabilitado |
| Timer no funciona | Verificar gameActive = true |
| No se puede agarrar | Verificar tag "Pickup" |
| No se puede interactuar | Verificar tag "Interactable" |

---

**Última actualización**: 2024

