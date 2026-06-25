using Portfolio.Web.Models;
using Portfolio.Web.Models.Werdegang;

namespace Portfolio.Web.Services;

public sealed class VirtualFileSystem
{
    private readonly Dictionary<string, VirtualFile> _dateien = new();

    public IReadOnlyList<TreeNode> Wurzel { get; }
    public IReadOnlyList<VirtualFile> Tour { get; }
    public VirtualFile Standarddatei => _dateien["README.md"];

    public VirtualFile? Holen(string pfad) =>
        _dateien.TryGetValue(pfad, out var d) ? d : null;

    public VirtualFileSystem()
    {
        Registrieren(Readme());
        Registrieren(Werdegang());
        Registrieren(Teamleiter());
        Registrieren(Ausbilder());
        Registrieren(FullStack());
        Registrieren(Pipeline());

        Tour =
        [
            _dateien["README.md"],
            _dateien["lebenslauf/Werdegang.json"],
            _dateien["management/Teamleiter.cs"],
            _dateien["ausbildung/Ausbilder.cs"],
            _dateien["techstack/FullStack.json"],
            _dateien["infrastruktur/pipeline.yml"],
        ];

        Wurzel =
        [
            Ordner("lebenslauf",    Datei("lebenslauf/Werdegang.json")),
            Ordner("management",    Datei("management/Teamleiter.cs")),
            Ordner("ausbildung",    Datei("ausbildung/Ausbilder.cs")),
            Ordner("techstack",     Datei("techstack/FullStack.json")),
            Ordner("infrastruktur", Datei("infrastruktur/pipeline.yml")),
            Datei("README.md"),
        ];
    }

    public int TourNummer(VirtualFile f)
    {
        var i = IndexOf(f);
        return i < 0 ? 0 : i + 1;
    }

    public int TourGesamt => Tour.Count;

    public VirtualFile? Weiter(VirtualFile f)
    {
        var i = IndexOf(f);
        return i >= 0 && i + 1 < Tour.Count ? Tour[i + 1] : null;
    }

    public VirtualFile? Zurueck(VirtualFile f)
    {
        var i = IndexOf(f);
        return i > 0 ? Tour[i - 1] : null;
    }

    private int IndexOf(VirtualFile f)
    {
        for (var i = 0; i < Tour.Count; i++)
            if (Tour[i].Pfad == f.Pfad) return i;
        return -1;
    }

    private void Registrieren(VirtualFile f) => _dateien[f.Pfad] = f;

    private static TreeNode Ordner(string name, params TreeNode[] kinder) =>
        new() { Name = name, IstOrdner = true, Kinder = [.. kinder] };

    private TreeNode Datei(string pfad)
    {
        var f = _dateien[pfad];
        return new TreeNode { Name = f.Name, IstOrdner = false, Pfad = f.Pfad };
    }

    public IReadOnlyList<WerdegangEintrag> WerdegangDaten { get; } =
    [
        new("2012-08", "2021-07", "schule", "Allgemeine Hochschulreife (Abitur)",
            "Albert-Schweitzer-Gymnasium Sömmerda", Ort: "Sömmerda",
            Abschluss: new(Note: "2,8"),
            Beschreibung:
                "Am Albert-Schweitzer-Gymnasium in Sömmerda habe ich 2021 mein Abitur "
                + "abgelegt. Schon während der Schulzeit war für mich klar, dass mein Weg "
                + "in die Softwareentwicklung führen soll. Deshalb habe ich früh eigene "
                + "kleine Projekte umgesetzt und mich gezielt auf eine Ausbildung im "
                + "IT-Bereich vorbereitet."),

        new("2021-08", "2024-01", "ausbildung", "Fachinformatiker für Anwendungsentwicklung",
            "on-geo GmbH", Ort: "Erfurt",
            Highlight: "Verkürzt aufgrund sehr guter Leistungen",
            Abschluss: new(IhkPunkte: 88, Berufsschule: "1,2"),
            Beschreibung:
                "Meine Ausbildung zum Fachinformatiker für Anwendungsentwicklung bei der "
                + "on-geo GmbH habe ich aufgrund durchgehend sehr guter Leistungen verkürzt "
                + "und nach rund zweieinhalb statt drei Jahren abgeschlossen. Den "
                + "IHK-Abschluss habe ich mit 88 von 100 Punkten und die Berufsschule mit "
                + "einem Schnitt von 1,2 beendet. Schon in dieser Zeit habe ich produktiv "
                + "an Kundenprojekten mitgearbeitet und Verantwortung für eigene Features "
                + "übernommen."),

        new("2024-01", null, "beruf", "Softwareentwickler",
            "on-geo GmbH", Aktuell: true, Ort: "Erfurt",
            Beschreibung:
                "Direkt nach der Ausbildung wurde ich als Softwareentwickler übernommen. "
                + "In dieser Rolle habe ich an .NET-Anwendungen entlang des gesamten Stacks "
                + "gearbeitet, von der Datenbank über die API bis zur Oberfläche. Mit der "
                + "Zeit habe ich mich zunehmend in Architekturentscheidungen und die "
                + "technische Abstimmung im Team eingebracht."),

        new("2026-02", null, "beruf", "Ausbilder & Teamleiter",
            "on-geo GmbH", Aktuell: true, Ort: "Erfurt",
            Highlight: "Verantwortung für 5 Auszubildende (2 pro Lehrjahr)",
            Beschreibung:
                "Seit Februar 2026 verantworte ich als Teamleiter und Ausbilder ein Team "
                + "aus aktuell fünf Auszubildenden, zwei pro Lehrjahr. Mein Fokus liegt auf "
                + "klarer technischer Richtung, hoher Codequalität durch verbindliche "
                + "Reviews und der gezielten Weiterentwicklung jedes Einzelnen. Damit "
                + "schließt sich der Kreis: Ich begleite heute die Ausbildung in genau dem "
                + "Betrieb, in dem ich selbst gelernt habe."),
    ];

    private static VirtualFile Readme() => new()
    {
        Pfad = "README.md",
        Name = "README.md",
        Titel = "Hallo",
        Sprache = "markdown",
        Ansicht = EditorAnsicht.Hero,
        Inhalt = "",
    };

    private static VirtualFile Werdegang() => new()
    {
        Pfad = "lebenslauf/Werdegang.json",
        Name = "Werdegang.json",
        Titel = "Werdegang",
        Kontext =
            "Mein Weg in die IT war kein Zufall, sondern eine bewusste Entscheidung mit "
            + "klarem Ziel. Vom Abitur über eine verkürzte Ausbildung bis zur Teamleitung "
            + "sind keine fünf Jahre vergangen."
            + "\n\n"
            + "Jede Station habe ich genutzt, um Verantwortung zu übernehmen, bevor sie mir "
            + "formal übertragen wurde. Die folgende Übersicht zeigt die einzelnen "
            + "Stationen mit den wichtigsten Ergebnissen und Kennzahlen im Detail.",
        Sprache = "json",
        Ansicht = EditorAnsicht.Zeitstrahl,
        Inhalt =
"""
{
  "person": "Elias-Constantin Regber",
  "stationen": [
    {
      "von": "2012-08", "bis": "2021-07",
      "typ": "schule",
      "titel": "Allgemeine Hochschulreife (Abitur)",
      "institution": "Albert-Schweitzer-Gymnasium Sömmerda",
      "ort": "Sömmerda",
      "abschluss": { "note": "2,8" }
    },
    {
      "von": "2021-08", "bis": "2024-01",
      "typ": "ausbildung",
      "titel": "Fachinformatiker für Anwendungsentwicklung",
      "institution": "on-geo GmbH",
      "highlight": "Verkürzt aufgrund sehr guter Leistungen",
      "abschluss": { "ihkPunkte": 88, "berufsschule": "1,2" }
    },
    {
      "von": "2024-01", "bis": null,
      "typ": "beruf",
      "titel": "Softwareentwickler",
      "institution": "on-geo GmbH",
      "aktuell": true
    },
    {
      "von": "2026-02", "bis": null,
      "typ": "beruf",
      "titel": "Ausbilder & Teamleiter",
      "institution": "on-geo GmbH",
      "aktuell": true,
      "highlight": "Verantwortung für 5 Auszubildende, 2 pro Lehrjahr"
    }
  ]
}
""",
        Annotationen =
        [
            new(17, "Leistung",
                "Ausbildung von 3 auf rund 2,5 Jahre verkürzt: vorzeitige Zulassung wegen guter Leistungen."),
            new(18, "Abschluss",
                "88 von 100 IHK-Punkten (sehr gut), Berufsschulnote 1,2."),
            new(33, "Aktuell",
                "Seit Februar 2026 Teamleiter und Ausbilder bei der on-geo GmbH."),
        ],
    };

    private static VirtualFile Teamleiter() => new()
    {
        Pfad = "management/Teamleiter.cs",
        Name = "Teamleiter.cs",
        Titel = "Teamleitung",
        Kontext =
            "Als Teamleiter und Ausbilder verantworte ich ein Team aus aktuell fünf "
            + "Auszubildenden, zwei pro Lehrjahr. Meine Aufgabe ist es, technische Richtung "
            + "vorzugeben, Qualität sicherzustellen und jeden Einzelnen gezielt "
            + "weiterzuentwickeln."
            + "\n\n"
            + "Ich plane Sprints realistisch statt optimistisch, prüfe jeden Pull Request "
            + "persönlich und gebe in festen Abständen strukturiertes Feedback. Führung "
            + "heißt für mich nicht kontrollieren, sondern einen Rahmen schaffen, in dem "
            + "gute Arbeit entstehen kann."
            + "\n\n"
            + "Der folgende Code ist eine bewusste Metapher für genau diese Verantwortung: "
            + "Die Methoden und Eigenschaften stehen für meine tägliche Arbeit, die "
            + "Kennzahlen darin sind echt. Fahre mit der Maus über die markierten Zeilen, "
            + "um den Hintergrund zu jeder Entscheidung zu sehen.",
        Sprache = "csharp",
        Inhalt =
"""
namespace Portfolio.Management;

/// <summary>
/// Fachliche und disziplinarische Führung: Menschen entwickeln,
/// Qualität sichern, Wissen weitergeben.
/// </summary>
public sealed class Teamleiter : IFuehrungskraft, IMentor
{
    // Stammdaten
    public string Rolle       => "Teamleiter & Ausbilder";
    public string Arbeitgeber => "on-geo GmbH";
    public int    TeamGroesse       => 5;   // aktuell 5 Auszubildende
    public int    AzubisProLehrjahr => 2;   // 2 pro Lehrjahr (aktuell 1 im 3. LJ)

    private const double Fokusfaktor = 0.8;  // realistische Sprint-Planung

    // Agile Führung: Sprintsteuerung
    public Sprintergebnis SprintLeiten(Backlog backlog, Team team)
    {
        var commitment = team.Kapazitaet() * Fokusfaktor;
        return new Sprintergebnis(commitment, methode: "Scrum/Kanban-Hybrid");
    }

    // Code Reviews: Qualität vor Geschwindigkeit
    public ReviewErgebnis CodeReview(PullRequest pr) => pr switch
    {
        { Tests: false }           => ReviewErgebnis.Aendern("Tests fehlen"),
        { Architektur: not Clean } => ReviewErgebnis.Diskutieren("Schnitt prüfen"),
        _                          => ReviewErgebnis.Freigeben(),
    };

    // Mentoring: kontinuierliche Entwicklung
    public void Foerdern(Entwickler dev)
    {
        dev.LernzielSetzen();
        dev.RegelmaessigesFeedback(intervall: TimeSpan.FromDays(14));
    }
}
""",
        Annotationen =
        [
            new(12, "Führungsspanne",
                "Verantwortung für aktuell 5 Auszubildende: 2 pro Lehrjahr (derzeit 1 im 3., je 2 im 1. und 2. Lehrjahr)."),
            new(13, "Ausbildung",
                "Zwei Azubis je Jahrgang: Onboarding, Git-Schulung und Prüfungsvorbereitung."),
            new(15, "Erfahrungswert",
                "80 % Fokusfaktor statt 100 %: Puffer für Reviews, Support und Meetings."),
            new(21, "Methodik",
                "Scrum/Kanban-Hybrid, angepasst an Team und Projektphase statt dogmatisch."),
            new(25, "Qualitätssicherung",
                "Pull-Request-Pflicht: kein Merge ohne grüne Tests und sauberen Schnitt."),
            new(36, "Mentoring",
                "Strukturiertes Feedback im Zwei-Wochen-Takt mit individuellen Lernzielen."),
        ],
    };

    private static VirtualFile Ausbilder() => new()
    {
        Pfad = "ausbildung/Ausbilder.cs",
        Name = "Ausbilder.cs",
        Titel = "Ausbildung",
        Kontext =
            "Ich bin zertifizierter Ausbilder mit AdA-Schein und betreue durchgehend zwei "
            + "Auszubildende pro Lehrjahr bis zur IHK-Abschlussprüfung. Ausbildung bedeutet "
            + "für mich Praxis statt Folien."
            + "\n\n"
            + "Meine Azubis arbeiten ab dem ersten Tag mit echtem Code und demselben "
            + "Git-Workflow wie das gesamte Team. Lernziele formuliere ich nach "
            + "didaktischen Prinzipien messbar und nachvollziehbar, sodass Fortschritt "
            + "sichtbar wird und nicht dem Zufall überlassen bleibt."
            + "\n\n"
            + "Da ich meinen eigenen Abschluss mit 88 von 100 Punkten gemacht habe, "
            + "bereite ich aus erlebter Erfahrung vor und nicht aus dem Lehrbuch. Die "
            + "markierten Zeilen zeigen, worauf es mir dabei konkret ankommt.",
        Sprache = "csharp",
        Inhalt =
"""
namespace Portfolio.Ausbildung;

/// <summary>
/// Ausbildung von Fachinformatikern (AE): vom ersten Commit
/// bis zur IHK-Abschlussprüfung. Fundiert durch den AdA-Schein.
/// </summary>
public sealed class Ausbilder : IMentor
{
    public string Qualifikation     => "AdA-Schein (Ausbildung der Ausbilder)";
    public int    AzubisProLehrjahr => 2;

    // Curriculum: praxisnah statt reiner Folien-Theorie
    public Lernpfad Curriculum() => new(
        grundlagen: ["C#", "Git", "Clean Code"],
        vertiefung: ["EF Core", "REST-APIs", "Blazor"],
        methode: "Pair Programming am echten Feature");

    // Git von Anfang an: Branch, Pull Request, Review, Merge
    public void GitSchulung(Azubi azubi)
    {
        azubi.Lernen("feature-branch -> Pull Request -> Review -> Merge");
        azubi.Ueben(commits: "klein, atomar, sprechende Messages");
    }

    // Didaktik: messbare Lernziele (AdA / Vier-Stufen-Methode)
    public Unterweisung KonzeptErstellen(string thema) => new(
        thema,
        lernziel: Lernziel.Smart(thema),   // spezifisch, messbar, terminiert
        methode: "Vier-Stufen-Methode");

    // Prüfungsvorbereitung
    public void PruefungVorbereiten(Azubi azubi)
        => azubi.Simulieren(pruefung: "IHK Abschluss Teil 1 & 2");
}
""",
        Annotationen =
        [
            new(9, "Formale Eignung",
                "AdA-Schein nach AEVO: didaktisch und rechtlich qualifizierter Ausbilder."),
            new(13, "Praxisnah",
                "Gelernt wird am echten Feature, nicht an isolierten Wegwerf-Übungen."),
            new(21, "Git-Kompetenz",
                "Azubis arbeiten ab Tag 1 im Branch- und PR-Workflow des gesamten Teams."),
            new(28, "Didaktik",
                "SMART-Lernziele aus dem AdA-Schein: spezifisch, messbar, terminiert."),
            new(33, "Aus Erfahrung",
                "Eigener Abschluss mit 88 IHK-Punkten: Vorbereitung aus erlebter Prüfungssituation."),
        ],
    };

    private static VirtualFile FullStack() => new()
    {
        Pfad = "techstack/FullStack.json",
        Name = "FullStack.json",
        Titel = "Tech-Stack",
        Kontext =
            "Mein technischer Schwerpunkt liegt klar auf C# und .NET: vom Backend mit "
            + "klassischen Controller-APIs und modernen Minimal APIs über Entity Framework "
            + "Core bis zu mehreren Frontend-Frameworks wie Blazor, Angular und React."
            + "\n\n"
            + "Werkzeuge wähle ich nach dem Projektkontext aus und nicht nach Trend. Eine "
            + "saubere Architektur mit klaren Schichten ist mir dabei genauso wichtig wie "
            + "die Geschwindigkeit, mit der ein Feature ausgeliefert werden kann."
            + "\n\n"
            + "Ergänzend kümmere ich mich um Containerisierung mit Docker, um CI/CD und "
            + "betreibe ein eigenes Homelab als Sandbox. Die folgende Übersicht fasst "
            + "meinen Werkzeugkasten zusammen; die markierten Zeilen erklären, warum "
            + "bestimmte Entscheidungen so getroffen sind.",
        Sprache = "json",
        Inhalt =
"""
{
  "entwickler": "Elias-Constantin Regber",
  "rolle": "Full-Stack Developer · Teamleiter · Ausbilder",
  "schwerpunkt": ".NET Enterprise / Web",
  "backend": {
    "sprache": "C# / .NET",
    "apis": {
      "controller": "Klassische ASP.NET Core Controller-APIs",
      "minimal": "Moderne Minimal APIs",
      "doku": [ "OpenAPI", "Swagger" ]
    },
    "daten": {
      "orm": "Entity Framework Core",
      "ecosystem": "Enterprise-NuGet-Pakete"
    },
    "architektur": [ "Clean Architecture", "Vertical Slice Architecture" ]
  },
  "frontend": {
    "frameworks": [ "Blazor", "Angular", "React", "ASP.NET Core MVC" ],
    "uiBibliotheken": [ "MudBlazor", "Radzen", "Tailwind CSS" ]
  },
  "devops": {
    "versionierung": "Git",
    "ciCd": [ "Azure DevOps", "CI/CD-Pipelines" ],
    "container": [ "Docker", "Docker Compose" ],
    "homelab": "Proxmox (Sandbox / Self-Hosting)"
  },
  "fuehrung": {
    "methoden": [ "Scrum", "Kanban" ],
    "praktiken": [ "Code Reviews", "Mentoring", "Technische Konzeption" ],
    "ausbildung": "AdA-Schein (Ausbildung der Ausbilder)"
  }
}
""",
        Annotationen =
        [
            new(16, "Architektur",
                "Clean Architecture für klare Schichten, Vertical Slice für feature-zentrierten Schnitt."),
            new(19, "Frontend-Breite",
                "Mehrere SPA-Frameworks produktiv, Auswahl nach Projektkontext statt Dogma."),
            new(24, "Automatisierung",
                "Azure DevOps Pipelines: Build, Test und Deploy ohne manuelle Schritte."),
            new(26, "Eigeninitiative",
                "Proxmox-Homelab als Sandbox für Container, Netzwerk und CI-Experimente."),
        ],
    };

    private static VirtualFile Pipeline() => new()
    {
        Pfad = "infrastruktur/pipeline.yml",
        Name = "pipeline.yml",
        Titel = "DevOps & Auslieferung",
        Kontext =
            "Sauberer Code ist nur die halbe Miete: Er muss zuverlässig und wiederholbar "
            + "in Produktion kommen. Diese Pipeline zeigt meinen Standard dafür."
            + "\n\n"
            + "Jeder Push auf den Hauptzweig löst automatisch einen Build aus, der "
            + "getestet wird, bevor irgendetwas ausgeliefert wird. Schlägt ein Test fehl, "
            + "gibt es kein Deployment. Der Produktivschritt ist zusätzlich durch Freigaben "
            + "abgesichert."
            + "\n\n"
            + "So entsteht ein nachvollziehbarer, vollständig automatisierter Weg von der "
            + "Codeänderung bis zum laufenden System, ganz ohne manuelle Handgriffe. Die "
            + "markierten Zeilen heben die wichtigsten Stationen dieses Weges hervor.",
        Sprache = "yaml",
        Inhalt =
"""
# Azure DevOps Pipeline: generisches .NET-Enterprise-Setup
# Stufen: Build & Test -> Deploy (Static Hosting)

trigger:
  branches:
    include: [ main ]

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'
  dotnetVersion: '10.0.x'

stages:
  - stage: Build
    displayName: 'Build & Test'
    jobs:
      - job: build
        steps:
          - task: UseDotNet@2
            inputs:
              version: $(dotnetVersion)
          - script: dotnet restore
            displayName: 'Restore'
          - script: dotnet build -c $(buildConfiguration) --no-restore
            displayName: 'Build'
          - script: dotnet test -c $(buildConfiguration) --no-build
            displayName: 'Unit Tests'
          - script: dotnet publish -c $(buildConfiguration) -o $(Build.ArtifactStagingDirectory)
            displayName: 'Publish'
          - publish: $(Build.ArtifactStagingDirectory)
            artifact: webapp

  - stage: Deploy
    displayName: 'Deploy (Static Hosting)'
    dependsOn: Build
    condition: succeeded()
    jobs:
      - deployment: deployWeb
        environment: 'production'
        strategy:
          runOnce:
            deploy:
              steps:
                - download: current
                  artifact: webapp
                - script: echo "wwwroot -> Static Host veroeffentlichen"
                  displayName: 'Deploy'
""",
        Annotationen =
        [
            new(4, "CI-Auslöser",
                "Jeder Push auf main startet die Pipeline automatisch."),
            new(13, "Ziel-Runtime",
                ".NET 10 (LTS): langfristiger Enterprise-Support."),
            new(28, "Qualitätsgate",
                "Schlagen Tests fehl, bricht die Pipeline ab: kein grünes Deploy auf rotem Code."),
            new(38, "Gate",
                "Deploy nur nach erfolgreichem Build und Test (condition: succeeded)."),
            new(41, "Freigabe",
                "Environment 'production' erlaubt Approvals und Checks vor dem Produktiv-Deploy."),
        ],
    };
}
