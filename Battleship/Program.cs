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
        if (orientacion == 0) //horizontal
        {
            tableroOculto[fila, columna + i] = reliquia[i];
        }
        else
        {
            tableroOculto[fila + i, columna] = reliquia[i];
        }
    }
}




//PROGRAMA PRINCIPAL

int tamanioTablero = 10;

string[] reliquias = { "S", "F", "ZZ", "MM", "EEE", "PPP", "RRRR", "BBBBB" };

char[,] tableroOculto = inicializarTablero(tamanioTablero);
char[,] tableroVisible = inicializarTablero(tamanioTablero);

posicionarReliquias(tableroOculto, reliquias, tamanioTablero);



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