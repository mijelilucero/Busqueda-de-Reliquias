static char[,] inicializarTablero(int tamanioTablero)
{
    char[,] tablero = new char[tamanioTablero, tamanioTablero];
    
    for (int i = 0; i < tamanioTablero; i++)
    {
        for (int j = 0; j < tamanioTablero; j++)
        {
            tablero[i, j] = '~';
        }
    }

    return tablero;
}


static void posicionarReliquias(char[,] tableroOculto, string[]reliquias, int tamanioTablero)
{
    Random random = new Random();

    foreach (string reliquia in reliquias)
    {
        bool reliquiaColocada = false;

        while (!reliquiaColocada)
        {
            int tamanioReliquia = reliquia.Length;
            int fila = random.Next(tamanioTablero);
            int columna = random.Next(tamanioTablero);
            int orientacion = random.Next(2); //0 horizontal y 1 vertical

            if (posicionDisponible(tableroOculto, tamanioReliquia, fila, columna, orientacion, tamanioTablero)==true)
            {
                colocarReliquia(tableroOculto, reliquia, tamanioReliquia, fila, columna, orientacion);
                reliquiaColocada = true;
            }
        }
    }
}


static bool posicionDisponible (char[,] tableroOculto, int tamanioReliquia, int fila, int columna, int orientacion, int tamanioTablero)
{
    if (fila + tamanioReliquia > tamanioTablero || columna + tamanioReliquia > tamanioTablero)
    {
        return false;
    }

    for (int i = 0; i < tamanioReliquia; i++)
    {
        if (orientacion == 0)
        {
            if (tableroOculto[fila, columna + i] != '~')
            {
                return false;
            }
        }
        else
        {
            if (tableroOculto[fila+i, columna] != '~')
            {
                return false;
            }
        }
    }


    return true;
}


static void colocarReliquia(char[,]tableroOculto, string reliquia, int tamanioReliquia, int fila, int columna, int orientacion)
{
    for (int i = 0; i < tamanioReliquia; i++)
    {
        if (orientacion == 0) 
        {
            tableroOculto[fila, columna + i] = reliquia[i];
        }
        else
        {
            tableroOculto[fila + i, columna] = reliquia[i];
        }
    }
}


static void mostrarTablero(char[,] tableroVisible, int tamanioTablero)
{
    Console.Write("    ");
    for (int i = 0; i < tamanioTablero; i++)
    {
        Console.Write($"{i} ");
    }
    Console.WriteLine();

    Console.WriteLine("   " + new string('-', tamanioTablero * 2 + 1));
    
    for (int i = 0; i < tamanioTablero; i++)
    {
        char letraFila = (char)('A' + i);
        Console.Write($"{letraFila}  ");
        for (int j = 0; j < tamanioTablero; j++)
        {
            Console.Write($"|{tableroVisible[i, j]}");
        }
        Console.WriteLine("|");
    }

    Console.WriteLine("   " + new string('-', tamanioTablero * 2 + 1));
}


static bool validarCoordenada(string coordenada)
{
    if (coordenada.Length != 2)
    {
        return false;
    }

    char fila = char.ToUpper(coordenada[0]);

    if (fila < 'A' || fila > 'J')
    {
        return false;
    }

    int columna = int.Parse(coordenada.Substring(1));

    if (columna < 0 || columna > 9)
    {
        return false;
    }


    return true;
}


static int convertirFila(char fila)
{
    int indiceFila = 0;

    switch (fila)
    {
        case 'A':
            indiceFila = 0;
            break;

        case 'B':
            indiceFila = 1;
            break;

        case 'C':
            indiceFila = 2;
            break;

        case 'D':
            indiceFila = 3;
            break;

        case 'E':
            indiceFila = 4;
            break;

        case 'F':
            indiceFila = 5;
            break;

        case 'G':
            indiceFila = 6;
            break;

        case 'H':
            indiceFila = 7;
            break;

        case 'I':
            indiceFila = 8;
            break;

        case 'J':
            indiceFila = 9;
            break;

        default:
            break;
    }

    return indiceFila;
}


static void marcarTableroVisible(char fila, int columna, char[,] tableroOculto, char[,] tableroVisible, int[] conteoReliquiasEncontradas)
{
    int indiceFila, indiceColumna;

    indiceFila = convertirFila(fila);
    indiceColumna = columna;

    if (tableroOculto[indiceFila, indiceColumna] == '~')
    {
        tableroVisible[indiceFila, indiceColumna] = 'x';
    }
    else
    {
        tableroVisible[indiceFila, indiceColumna] = tableroOculto[indiceFila, indiceColumna];
        acumularPartesDeReliquias(tableroOculto[indiceFila, indiceColumna], conteoReliquiasEncontradas);
    }
}


static void acumularPartesDeReliquias(char simbolo, int[] conteoReliquiasEncontradas)
{
    switch (simbolo)
    {
        case 'S':
            conteoReliquiasEncontradas[0]++;
            break;

        case 'F':
            conteoReliquiasEncontradas[1]++;
            break;

        case 'Z':
            conteoReliquiasEncontradas[2]++;
            break;

        case 'M':
            conteoReliquiasEncontradas[3]++;
            break;

        case 'E':
            conteoReliquiasEncontradas[4]++;
            break;

        case 'P':
            conteoReliquiasEncontradas[5]++;
            break;

        case 'R':
            conteoReliquiasEncontradas[6]++;
            break;

        case 'B':
            conteoReliquiasEncontradas[7]++;
            break;
    }
}


static void marcarReliquiaComoEncontrada(int[] conteoReliquiasEncontradas, bool[] reliquiasEncontradas)
{
    if (conteoReliquiasEncontradas[0] == 1) //S
    {
        reliquiasEncontradas[0] = true;
    }
    else if (conteoReliquiasEncontradas[1] == 1) //F
    {
        reliquiasEncontradas[1] = true;
    }
    else if (conteoReliquiasEncontradas[2] == 2) //ZZ
    {
        reliquiasEncontradas[2] = true;
    }
    else if (conteoReliquiasEncontradas[3] == 2) //MM
    {
        reliquiasEncontradas[3] = true;
    }
    else if (conteoReliquiasEncontradas[4] == 3) //EEE
    {
        reliquiasEncontradas[4] = true;
    }
    else if (conteoReliquiasEncontradas[5] == 3) //PPP
    {
        reliquiasEncontradas[5] = true;
    }
    else if (conteoReliquiasEncontradas[6] == 4) //RRRR
    {
        reliquiasEncontradas[6] = true;
    }
    else if (conteoReliquiasEncontradas[7] == 5) //BBBBB
    {
        reliquiasEncontradas[7] = true;
    }
}


//PROGRAMA PRINCIPAL

int tamanioTablero = 10;
int maxIntentos = 30;

string[] reliquias = { "S", "F", "ZZ", "MM", "EEE", "PPP", "RRRR", "BBBBB" };

int[] conteoReliquiasEncontradas = new int[reliquias.Length];
bool[] reliquiasEncontradas=new bool[reliquias.Length];

for (int i = 0; i < reliquias.Length; i++)
{
    conteoReliquiasEncontradas[i] = 0;
}   

char[,] tableroOculto = inicializarTablero(tamanioTablero);
char[,] tableroVisible = inicializarTablero(tamanioTablero);

posicionarReliquias(tableroOculto, reliquias, tamanioTablero);

bool juegoTerminado = false;
int intentos = 0;
int puntaje = 0;

bool[,] coordenadasUtilizadas=new bool[tamanioTablero,tamanioTablero]; 

while (!juegoTerminado)
{
    int intentosRestantes = maxIntentos - intentos;
    Console.WriteLine($"\n\nTe quedan {intentosRestantes} intentos restantes.");
    Console.WriteLine($"Llevas {puntaje} puntos.");

    mostrarTablero(tableroVisible, tamanioTablero);

    Console.WriteLine("\n\nTablero oculto");
    for (int i = 0; i < tamanioTablero; i++)
    {
        for (int j = 0; j < tamanioTablero; j++)
        {
            Console.Write("{0,3}", tableroOculto[i, j]);
        }
        Console.WriteLine();
    }


    Console.WriteLine("\n\nIngresa una coordenada para atacar (ej. A0):");
    string coordenada = Console.ReadLine();

    if (validarCoordenada(coordenada) == true)
    {
        char fila = char.ToUpper(coordenada[0]);
        int columna = int.Parse(coordenada.Substring(1));

        if (coordenadasUtilizadas[convertirFila(fila), columna] == true)
        {
            Console.WriteLine("Coordenada ya utilizada intente de nuevo.");
            continue;
        }

        coordenadasUtilizadas[convertirFila(fila), columna] = true;        
        marcarTableroVisible(fila, columna, tableroOculto, tableroVisible, conteoReliquiasEncontradas);

        intentos++;

        marcarReliquiaComoEncontrada(conteoReliquiasEncontradas, reliquiasEncontradas);

        for (int i = 0; i < reliquias.Length; i++)
        {
            if (reliquiasEncontradas[i] == true)
            {
                Console.WriteLine($"¡Has encontrado la reliquia {reliquias[i]} completa!");
            }
        }


        if (intentos == 30)
        {
            juegoTerminado = true;
            Console.WriteLine("¡Has encontrado todas las reliquias! ¡Felicidades!");
        }
    }
    else
    {
        Console.WriteLine("Coordenada inválida. Inténtelo de nuevo.");
    }
}







//Console.WriteLine("\n\nTablero oculto");
//for (int i = 0; i < tamanioTablero; i++)
//{
//    for (int j = 0; j < tamanioTablero; j++)
//    {
//        Console.Write("{0,3}", tableroOculto[i, j]);
//    }
//    Console.WriteLine();
//}

//Console.WriteLine("\n\nTablero visible");
//for (int i = 0; i < tamanioTablero; i++)
//{
//    for (int j = 0; j < tamanioTablero; j++)
//    {
//        Console.Write("{0,3}", tableroVisible[i, j]);
//    }
//    Console.WriteLine();
//}