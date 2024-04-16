using System;
using System.Collections.Generic;
using System.IO;

public class Etudiant
{
    public int Numero { get; set; }
    public string Nom { get; set; }
    

    public Etudiant(string nom, int numero)
    {
        Nom = nom;
        Numero = numero;
    }
}

public class Cours
{
    public string Nom { get; set; }
    public int Code { get; set; }

    public Cours(string nom, int code)
    {
        Nom = nom;
        Code = code;
    }
}

public class Note
{
    public int NumeroEtudiant { get; set; }
    public int CodeCours { get; set; }
    public double Valeur { get; set; }

    public Note(int numeroEtudiant, int codeCours, double valeur)
    {
        NumeroEtudiant = numeroEtudiant;
        CodeCours = codeCours;
        Valeur = valeur;
    }
}

public class GestionDeNotes
{
    private List<Cours> cours;
    private List<Etudiant> etudiants;
    private List<Note> notes;

    public GestionDeNotes()
    {
        cours = new List<Cours>();
        etudiants = new List<Etudiant>();
        notes = new List<Note>();
    }

    public void AjouterEtudiant(string nom, int numero)
    {
        etudiants.Add(new Etudiant(nom, numero));
    }

    public void AjouterCours(string nom, int code)
    {
        cours.Add(new Cours(nom, code));
    }
    public IEnumerable<Etudiant> ObtenirEtudiants()
    {
        return etudiants.AsReadOnly();
    }

    public void SaisirNote()
    {
       

        int numeroEtudiant, codeCours;
        double valeur;

        Console.Write("veuillez donner le numéro de l'étudiant concerné: ");
        while (!int.TryParse(Console.ReadLine(), out numeroEtudiant) || !EtudiantExiste(numeroEtudiant))
        {
            Console.WriteLine("le numéro de l'étudiant saisie est  invalide. Veuillez réessayez svp.");
            Console.Write("veuillez entrer un nouveau numéro : ");
        }

        Console.Write("veuillez donner une note ");
        while (!double.TryParse(Console.ReadLine(), out valeur) || valeur < 0 || valeur > 100)
        {
            Console.WriteLine("La note est invalide. La note doit être comprise entre 0 et 100. Veuillez réessayer svp.");
            Console.Write("La note est : ");
        }

        Console.Write("Donne le Code du cours: ");
        while (!int.TryParse(Console.ReadLine(), out codeCours) || !CoursExiste(codeCours))
        {
            Console.WriteLine("Ce code est invalide.Veuillez Réessayer svp.");
            Console.Write("Donne un code valide ");
        }


        notes.Add(new Note(numeroEtudiant, codeCours, valeur));
    }

    private bool EtudiantExiste(int numero)
    {
        foreach (var etudiant in etudiants)
        {
            if (etudiant.Numero == numero)
                return true;
        }
        return false;
    }

    private bool CoursExiste(int code)
    {
        foreach (var cours in cours)
        {
            if (cours.Code == code)
                return true;
        }
        return false;
    }

    public void EnregistrerNotesDansFichier()
    {
        foreach (var etudiant in etudiants)
        {
            using (StreamWriter writer = new StreamWriter($"notes_{etudiant.Numero}.txt"))
            {
                foreach (var note in notes)
                {
                    if (note.NumeroEtudiant == etudiant.Numero)
                    {
                        writer.WriteLine($"Cours: {note.CodeCours}, Note: {note.Valeur}");
                    }
                }
            }
        }
    }

    public void AfficherReleveDeNotes(int numeroEtudiant)
    {
        foreach (var etudiant in etudiants)
        {
            if (etudiant.Numero == numeroEtudiant)
            {
                Console.WriteLine($"voici le relevé de notes pour l'étudiant {etudiant.Nom}:");
                foreach (var note in notes)
                {
                    if (note.NumeroEtudiant == numeroEtudiant)
                    {
                        Console.WriteLine($"Cours: {note.CodeCours}, Note: {note.Valeur}");
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GestionDeNotes gestionDesNotes = new GestionDeNotes();

        gestionDesNotes.AjouterEtudiant("Adja", 1);
        gestionDesNotes.AjouterEtudiant("Oumou", 2);
        gestionDesNotes.AjouterEtudiant("Aby", 3);
        gestionDesNotes.AjouterEtudiant("Fatou", 4);
        gestionDesNotes.AjouterCours("Francais", 101);
        gestionDesNotes.AjouterCours("Sciences", 202);
        gestionDesNotes.AjouterCours("Programmation", 301);
        gestionDesNotes.AjouterCours("Statistique", 401);

        Console.WriteLine(" entrez des notes pour les etudiants");
        for (int i = 0; i < 4; i++)
        {
            gestionDesNotes.SaisirNote();
        }
        gestionDesNotes.EnregistrerNotesDansFichier();

        Console.WriteLine("Voici le relevé de notes de tous les étudiants :");
        foreach (var etudiant in gestionDesNotes.ObtenirEtudiants())
        {
            gestionDesNotes.AfficherReleveDeNotes(etudiant.Numero);
            Console.WriteLine();


          

      
      
    }
    } }

