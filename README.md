# MazeRunners
 Proyecto de programacion, juego de escape del laberinto

# Descripcion
 Maze Runners es un juego multijugador de consola, donde tu objetivo es llegar primero al centro del laberinto

# Como jugar
 El juego consta con una seccion donde detalladamente explica como jugar, que piezas puedes escoger, asi como las trampas que te vas a encontrar en el laberinto

# Logica del juego
  _Clase GameManager_ controla el flujo del juego desde el inicio, el menu de usuarios, la seleccion de personajes, la generacion de las trampas y el desarrollo del juego mientras no se cumpla la condicion de victoria
 
  _Clase Maze_ permite la generacion aleatoria y procedural del laberinto
  
  _Clase Box_ es la clase padre de la que heredan todas los distintos tipos de casillas que componen el laberinto
  
  _Clase Player_ contiene la informacion de los jugadores
  
  _Clase Piece_ guarda la posicion de la ficha,asi como controla el movimiento de esta a traves del laberinto
