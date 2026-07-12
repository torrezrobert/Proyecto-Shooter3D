# Proyecto Shooter 3D - Desarrollo Académico

Este repositorio contiene el código fuente y la documentación técnica de un proyecto de videojuego tipo shooter en primera persona desarrollado en Unity (C#).

---

## 🎮 Descripción del Proyecto
El proyecto consiste en un entorno interactivo de supervivencia y combate. Se ha diseñado con un enfoque en **arquitectura modular, optimización de recursos y escalabilidad**, integrando mecánicas de IA, sistemas de disparo y gestión de estados de juego.

## 🎯 Objetivos
El objetivo central fue construir un entorno funcional donde la interacción entre el jugador y los elementos del escenario fuera eficiente. Se priorizó la modularidad, permitiendo que cada componente funcione de manera independiente, facilitando futuras expansiones.

## 🛠 Características Técnicas

### 1. Decisiones de Arquitectura y Diseño
- **Arquitectura Basada en Eventos:** Se implementó una lógica de actualización reactiva. La interfaz (HUD) se actualiza solo ante eventos disparadores (daño/uso de ítems), reduciendo la carga del `Update()`.
- **Detección de Impactos (Raycast):** Sistema eficiente de combate que evita la instanciación innecesaria de objetos físicos (balas).
- **Gestión Asíncrona (Corrutinas):** Uso de `IEnumerator` para gestionar esperas (reaparición de botiquines) sin bloquear el hilo principal.
- **Principio DRY:** Centralización de la lógica de control de vida y reinicio para asegurar consistencia.

### 2. Implementación y Tecnologías
- **Inteligencia Artificial:** Implementación de `NavMesh` para navegación autónoma y eficiente de entidades.
- **Gestión de Assets:** Uso del formato `.glb` para integrar geometría y materiales en un solo contenedor binario.
- **Optimización Física:** Uso de `FixedUpdate` para garantizar estabilidad física independiente del hardware.

## 🚀 Análisis de Rendimiento
La optimización se centró en tres pilares:
1. **Cacheo de componentes:** Uso de `GetComponent` en `Start()` para evitar llamadas costosas durante la ejecución.
2. **Gestión de Jerarquía:** Control de scripts inactivos para limitar el procesamiento.
3. **Fluidez:** Ajuste de la lógica para mantener una tasa constante de 60 FPS en hardware estándar.

## 📂 Estructura de Scripts
- `Disparo.cs`: Lógica de combate y detección de impactos mediante Raycast.
- `Botiquin.cs`: Sistema de regeneración con reaparición asíncrona.
- `Vida.cs`: Gestor de salud del jugador y lógica de fin de partida/reinicio.
- `GestorVictoria.cs`: Control de condiciones de victoria.

## 📝 Licencia
Este proyecto es de carácter académico.

---
*Desarrollado por: Roberto Torrez Escalante (2026)*
