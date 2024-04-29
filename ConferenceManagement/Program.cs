using System;
using System.Collections.Generic;
using System.Text;

public class Palestra
{
    public string NomePalestra { get; set; }
    public int DuracaoPalestra { get; set; }

    public Palestra(string nomePalestra, int duracaoPalestra)
    {
        NomePalestra = nomePalestra;
        DuracaoPalestra = duracaoPalestra;
    }
}

public class Palestras
{
    public List<Palestra> ListaPalestras { get; set; }

    public Palestras()
    {
        ListaPalestras = new List<Palestra>();
    }

    public void AdicionarPalestra(string nomePalestra, int duracaoPalestra)
    {
        var palestra = new Palestra(nomePalestra, duracaoPalestra);        
        ListaPalestras.Add(palestra);
    }
}

public class Trilhas
{
    public List<string> ListaTrilhas { get; set; } = new List<string>();
    public int QntTrilhas { get; private set; }
    public List<Palestra> ListaPalestras { get; private set; }

    public Trilhas (List<Palestra> listaPalestras)
    {
        ListaPalestras = listaPalestras;
    }

    public void gerarTrilhas() {
        bool liberado = true;
        bool fimTrilha = false;
        int numeroTrilha = 1;
        List<Palestra> palestrasParaProcessar = new List<Palestra>(ListaPalestras);
        List<string> ListaTrilhas = new List<string>();
        TimeSpan TempoTrilha = TimeSpan.Parse("09:00");
        ListaTrilhas.Add("Trilha " + numeroTrilha + ":");
        while (liberado) {
            int index = 0;
            if(fimTrilha){
                ListaTrilhas.Add("\n");                      
                TempoTrilha = TimeSpan.Parse("09:00");
                numeroTrilha ++;
                ListaTrilhas.Add("Trilha " + numeroTrilha + ":");
            }
            while (index < palestrasParaProcessar.Count) {
                Palestra linha = palestrasParaProcessar[index];
                string nome = linha.NomePalestra;
                int duracao = linha.DuracaoPalestra;
                TimeSpan tmpTempoTrilha = TempoTrilha + TimeSpan.FromMinutes(duracao);

                if (TempoTrilha <= TimeSpan.Parse("12:00")) {
                    if (tmpTempoTrilha <= TimeSpan.Parse("12:00")) {
                        if (duracao == 5) {
                            ListaTrilhas.Add(TempoTrilha.ToString("hh\\:mm") + "H " + nome + " relâmpago");
                        } else {
                            ListaTrilhas.Add(TempoTrilha.ToString("hh\\:mm") + "H " + nome + " " + duracao + "min");
                        }
                        TempoTrilha = tmpTempoTrilha;
                        palestrasParaProcessar.RemoveAt(index);
                    } else {
                        TempoTrilha = TimeSpan.Parse("13:00");
                        index++;
                    }
                } else if (TempoTrilha >= TimeSpan.Parse("13:00") && tmpTempoTrilha <= TimeSpan.Parse("17:00")) {
                    if (duracao == 5) {
                        ListaTrilhas.Add(TempoTrilha.ToString("hh\\:mm") + "H " + nome + " relâmpago");
                    } else {
                        ListaTrilhas.Add(TempoTrilha.ToString("hh\\:mm") + "H " + nome + " " + duracao + "min");
                    }
                    TempoTrilha = tmpTempoTrilha;
                    palestrasParaProcessar.RemoveAt(index);
                } else {
                    index++;
                }

                if(TempoTrilha <= TimeSpan.Parse("16:00")){
                    fimTrilha = false;
                }else{
                    fimTrilha = true;                    
                }
            }
            if (palestrasParaProcessar.Count == 0) {
                liberado = false;
            }
        }

        foreach (var trilha in ListaTrilhas) {
            Console.WriteLine(trilha);
        }
    }
}

public class ConferenceManager
{
    public static void Main()
    {
        Console.WriteLine("Cole suas palestras (pressione Enter em uma linha vazia para terminar):");

        StringBuilder ListaPalestras = new StringBuilder();
        string inputLine;

        var Palestras = new Palestras();
        while (true)
        {
            inputLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
            {
                break;
            }

            string nomePalestra = inputLine.Substring(0, inputLine.LastIndexOf(' '));
            string tmpDuracaoPalestra = inputLine.Substring(inputLine.LastIndexOf(' ') + 1);
            int duracaoPalestra;
            try {
                duracaoPalestra = tmpDuracaoPalestra == "relampago" ? 5 : int.Parse(tmpDuracaoPalestra.Substring(0, tmpDuracaoPalestra.Length - 3));
            } catch (FormatException ex) {
                duracaoPalestra = 5;
            }            
            
            Palestras.AdicionarPalestra(nomePalestra,duracaoPalestra);
        }

        var Trilhas = new Trilhas(Palestras.ListaPalestras);
        Trilhas.gerarTrilhas();
    }
}