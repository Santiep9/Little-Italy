# Movimiento del Personaje 

    El personaje se mueve en ambos ejes


![Little Italy - TestScene - Windows, Mac, Linux - Unity 6 2 (6000 2 7f2) _DX12_ 2025-11-25 23-28-00](https://github.com/user-attachments/assets/5cae7a8c-106b-4144-a68f-5c3188d1e51c)





# Armas y disparo 

    Encontramos diferentes tipos de disparo con los que segun el arma que tengamos, 
    el tag de la bala se cambia para ejercer un mayor o menor daño

![Little Italy - TestScene - Windows, Mac, Linux - Unity 6 2 (6000 2 7f2) _DX12_ 2025-11-25 23-28-00](https://github.com/user-attachments/assets/3e1edf91-8938-4fa9-b42d-f9e5e87e7d30)


# Tipos de arma
Glock

    Este arma hace 10 de daño al enemigo, necesitando 10 balas para matarlo

![Little Italy - TestScene - Windows, Mac, Linux - Unity 6 2 (6000 2 7f2) _DX12_ 2025-11-25 23-28-00](https://github.com/user-attachments/assets/4c401f66-be6d-47ac-8738-8fed90482836)

Sniper
-Este arma hace 100 de daño al enemigo, matandolo de 1 solo disparo

![Little Italy - TestScene - Windows, Mac, Linux - Unity 6 2 (6000 2 7f2) _DX12_ 2025-11-25 23-28-00_1](https://github.com/user-attachments/assets/10a36a0b-ab39-48ca-bd97-70ca0835fef2)


Se espera incluir un arma más en un futuro

# Potenciadores

-Velocidad

    La bola verde por ahora es un potenciador que incrementa la velocidad del personaje

![Little Italy - TestScene - Windows, Mac, Linux - Unity 6 2 (6000 2 7f2) _DX12_ 2025-11-25 23-28-00_1](https://github.com/user-attachments/assets/f17bf277-4eb1-49ef-a3bd-e5923b54af44)


# Enemigos 
-Hay tres tipos de enemigos:

    -Enemigo que te sigue:
    Este enemigo te sigue por el mapa mientras te dispara

![Little Italy - TestScene - Windows, Mac, Linux - Unity 6 2 (6000 2 7f2) _DX12_ 2025-11-25 23-28-00_1](https://github.com/user-attachments/assets/161209d3-f305-4f48-9d12-cf1c2cbf7f1f)

Se espera añadir 2 enemigos más al juego


# Level Design

      Nivel provisional del juego inspirado en el de hotline miami

<img width="778" height="447" alt="image" src="https://github.com/user-attachments/assets/4fbbeac4-e396-443f-afc4-5b7069804a0e" />

## Nombres:
RKPlane --> Alberto

Santiep9 --> Santi

maizzon14 --> Marcos

Felix32323232 --> Felix

fjnl2006 --> Fran

HERNI47 --> Hernan

# Implementación de probabilidad
## Probabilidad uniforme

Script RandomWeapon.cs

En el script RandomWeapon.cs se utiliza probabilidad uniforme para seleccionar un arma inicial al comenzar la partida.
Cada arma del array randomWeapons tiene la misma probabilidad de ser elegida.
Random.Range(0, n) genera un entero uniforme en el rango [0, n).
Después, solo el arma seleccionada se activa:

## Tipo de probabilidad
### Uniforme discreta

Coste temporal: O(n)
Coste espacial: O(1)

# Método iterativo y versión recursiva
## Método iterativo implementado
Script ConoShotgun.cs

El método recorre iterativamente todos los rayos del cono de disparo de la escopeta para:
Detectar colisiones
Identificar enemigos
Aplicar daño solo una vez por disparo

Coste temporal: O(n), siendo n el número de rayos
Coste espacial: O(1)
Legibilidad: Alta
Uso recomendado en tiempo real

## Versión recursiva (no usada)

```csharp
void ProcessRay(int index)
{
    if (index > rayCount) return;

    // lógica del rayo
    ProcessRay(index + 1);
}
```

Coste temporal: O(n)
Coste espacial: O(n)

Nos quedaríamos con la versión iterativa, ya que:
Consume menos memoria
Es más clara
Es más segura para ejecución por frame

# Estrategia algorítmica
## Estrategia: Divide y vencerás

Script: ConoShotgun.cs

El disparo de la escopeta divide el área total de ataque en múltiples sub-problemas independientes:
El cono se divide en rayCount rayos
Cada rayo se evalúa de forma independiente
Los resultados se combinan para construir el daño total
Cada rayo es una “sub-solución” del problema general.

Coste temporal: O(n)
Coste espacial: O(1)
Ventaja: Modularidad y precisión configurable

# Árbol lógico de estados del jugador

Script: Player.cs

El jugador sigue una estructura de árbol de estados implícito:

<p align="center">
  <img src="ArbolBinario.png" width="500">
</p>

Implementación en código:
```csharp
if (invincible) return;
if (!canTakeDamage) return;
if (Health <= 0) Die();
```

Esto controla:
Control de daño
Transiciones claras entre estados
Fácil ampliación futura (stun, dash, power-ups)

# Conclusion final:
El proyecto implementa correctamente:
Probabilidad uniforme
Algoritmos iterativos
Estrategia divide y vencerás
Estructura de árbol de estados
Análisis de costes algorítmicos


