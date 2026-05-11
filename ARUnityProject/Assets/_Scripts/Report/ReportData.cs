using System;
using System.Collections.Generic;

[Serializable]
public class RigaDifetto
{
    public string Codice;
    public string Descrizione;
    public int Gravità;

    public bool? Visto;

    public bool? Estensione02;
    public bool? Estensione05;
    public bool? Estensione1;

    public bool? Intensità02;
    public bool? Intensità05;
    public bool? Intensità1;

    public bool? PS;
    public bool? NA;
    public bool? NR;
    public bool? NP;
}


[Serializable]
public class GruppoDifetti
{
    public string Elemento;
    public List<RigaDifetto> RigheDifetto = new();
}


[Serializable]
public class IntestazioneReport
{
    public string AppCode;
    public string Codice;
    public string Marca;
    public string Veicolo;
    public string Tecnico1;
    public string Tecnico2;
    public string Data;

    public IntestazioneReport DefaultHeader()
    {
        var header = new IntestazioneReport
        {
            AppCode = "CAR.01",
            Codice = "0000",
            Marca = "Audi",
            Veicolo = "A7 Sportback 2018 55 TFSI",
            Tecnico1 = "Mario Rossi",
            Tecnico2 = "John Doe",
            Data = DateTime.Now.ToString("dd-MM-yyyy")
        };

        return header;
    }
}

[Serializable]
public class ReportDocument
{
    public IntestazioneReport Intestazione;
    public List<GruppoDifetti> Gruppi;
}