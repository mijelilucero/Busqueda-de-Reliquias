int tamanioTablero = 10;

string[] reliquias = { "S", "F", "ZZ", "MM", "EEE", "PPP", "RRRR", "BBBBB" };

char[,] tableroOculto = inicializarTablero(tamanioTablero);
char[,] tableroVisible = inicializarTablero(tamanioTablero);



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