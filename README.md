# MazeRunners
 Proyecto de programacion, juego de escape del laberinto

# Descripcion
 Maze Runners es un juego multijugador de consola, donde tu objetivo es llegar primero al centro del laberinto

# Como jugar
 El juego consta con una seccion donde detalladamente explica como jugar, que piezas puedes escoger, asi como las trampas que te vas a encontrar en el laberinto

# Logica del juego
  Clase GameManager controla el flujo del juego desde el inicio, el menu de usuarios, la seleccion de personajes, la generacion de las trampas y el desarrollo del juego mientras no se cumpla la condicion de victoria
  Clase Maze permite la generacion aleatoria y procedural del laberinto
  Clase Box es la clase padre de la que heredan todas los distintos tipos de casillas que componen el laberinto
  Clase Player contiene la informacion de los jugadores
  Clase Piece guarda la posicion de la ficha,asi como controla el movimiento de esta a traves del laberinto
