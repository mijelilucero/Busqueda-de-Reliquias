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


static int puntajePorReliquia(string reliquia)
{
    switch (reliquia.Length)
    {
        case 1:
            return 5;
        case 2:
            return 10;
        case 3:
            return 20;
        case 4:
            return 35;
        case 5:
            return 45;
    }

    return 0;
}


static void marcarReliquiaComoEncontrada(int[] conteoReliquiasEncontradas, string[] reliquias, bool[] reliquiasEncontradas, ref int puntaje, ref bool nuevaReliquiaEncontrada, ref string ultimaReliquiaEncontrada)
{
    if (!reliquiasEncontradas[0] && conteoReliquiasEncontradas[0] == 1) //S
    {
        reliquiasEncontradas[0] = true;
        puntaje += puntajePorReliquia(reliquias[0]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "AMULETO DEL SOL";
    }
    else if (!reliquiasEncontradas[1] && conteoReliquiasEncontradas[1] == 1) //F
    {
        reliquiasEncontradas[1] = true;
        puntaje += puntajePorReliquia(reliquias[1]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "SELLO DEL FARAÓN";
    }
    else if (!reliquiasEncontradas[2] && conteoReliquiasEncontradas[2] == 2) //ZZ
    {
        reliquiasEncontradas[2] = true;
        puntaje += puntajePorReliquia(reliquias[2]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "BRAZALETE DE SERPIENTES";
    }
    else if (!reliquiasEncontradas[3] && conteoReliquiasEncontradas[3] == 2) //MM
    {
        reliquiasEncontradas[3] = true;
        puntaje += puntajePorReliquia(reliquias[3]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "MÁSCARA CEREMONIAL";
    }
    else if (!reliquiasEncontradas[4] && conteoReliquiasEncontradas[4] == 3) //EEE
    {
        reliquiasEncontradas[4] = true;
        puntaje += puntajePorReliquia(reliquias[4]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "COFRE DE ESMERALDAS";
    }
    else if (!reliquiasEncontradas[5] && conteoReliquiasEncontradas[5] == 3) //PPP
    {
        reliquiasEncontradas[5] = true;
        puntaje += puntajePorReliquia(reliquias[5]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "ESPADA DE OBSIADIANA";
    }
    else if (!reliquiasEncontradas[6] && conteoReliquiasEncontradas[6] == 4) //RRRR
    {
        reliquiasEncontradas[6] = true;
        puntaje += puntajePorReliquia(reliquias[6]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "CETRO DE LOS REYES";
    }
    else if (!reliquiasEncontradas[7] && conteoReliquiasEncontradas[7] == 5) //BBBBB
    {
        reliquiasEncontradas[7] = true;
        puntaje += puntajePorReliquia(reliquias[7]);
        nuevaReliquiaEncontrada = true;
        ultimaReliquiaEncontrada = "BÁCULO DE LOS SABIOS";
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
bool nuevaReliquiaEncontrada = false;
string ultimaReliquiaEncontrada = "";

while (!juegoTerminado)
{
    int intentosRestantes = maxIntentos - intentos;
    Console.WriteLine($"\n\nTe quedan {intentosRestantes} intentos restantes.");
    Console.WriteLine($"Llevas {puntaje} puntos.");



    Console.WriteLine("\n\nTablero oculto");
    for (int i = 0; i < tamanioTablero; i++)
    {
        for (int j = 0; j < tamanioTablero; j++)
        {
            Console.Write("{0,3}", tableroOculto[i, j]);
        }
        Console.WriteLine();
    }



    mostrarTablero(tableroVisible, tamanioTablero);

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

        marcarReliquiaComoEncontrada(conteoReliquiasEncontradas, reliquias, reliquiasEncontradas, ref puntaje, ref nuevaReliquiaEncontrada, ref ultimaReliquiaEncontrada);

        if (nuevaReliquiaEncontrada)
        {
            Console.WriteLine($"¡Has encontrado la reliquia {ultimaReliquiaEncontrada} completa!");
            nuevaReliquiaEncontrada = false;
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