using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        //Bilder werden geladen.
        Image bomb = Image.FromFile(".\\bomb.png");
        Image blast = Image.FromFile(".\\blast.png");
        Image wert1 = Image.FromFile(".\\1.png");
        Image wert2 = Image.FromFile(".\\2.png");
        Image wert3 = Image.FromFile(".\\3.png");
        Image wert4 = Image.FromFile(".\\4.png");
        Image wert5 = Image.FromFile(".\\5.png");
        Image wert6 = Image.FromFile(".\\6.png");
        Image wert7 = Image.FromFile(".\\7.png");
        Image wert8 = Image.FromFile(".\\8.png");
        Image unflagged = Image.FromFile(".\\unflagged.png");
        Image flagged = Image.FromFile(".\\flagged.png");
        Image questionMark = Image.FromFile(".\\mark.png");
        Image playingSmiley = Image.FromFile(".\\thinking.png");
        Image looseSmiley = Image.FromFile(".\\loose.png");
        Image woonSmiley = Image.FromFile(".\\woon.png");

        //für das Spielfeld wird ein 2 dimensionales Array mit "feldButton" Klasse erstellt
        // Die Breite und Höhe sind "statisch" definiert.
        feldButton[,] felder = new feldButton[breite, hoehe];
        static int breite = 20;
        static int hoehe = 20;

        //Alle Bomben, die auf dem Spielfeld platziert werden,
        //kommen nachher auf die Liste "bomben".
        List<feldButton> bomben = new List<feldButton>();

        //Mit Listen arbeiten ist viel schöner und einfacher aber ohne "Array" 
        //ist es auch manche Sachen nicht einfach zu realisieren.
        //Darum wird hier noch eine Liste erstellt, die alle einzelne Felder enthalten.
        List<feldButton> lstFelder = new List<feldButton>();

        //Wird für Spieltimer benötigt.
        Timer timer = new Timer();
        //Wird für die zufällige Platzierung der Bomben gebraucht.
        Random rnd = new Random();

        // "false" wird gesetzt, wenn das Spiel gewonnen oder verloren wurde.
        bool spielAktiv = true;

        //die "Markierung" mit einer Flagge wird hier mitgezählt. 
        //Nach jeder Markierung wird dann auch geprüft ob alle Bomben markiert sind.
        //Damit in dem Fall das Spiel als "gewonnen" beendet wird.
        int flagCounter = 0;

        //sobald ein Feld "geöffnet" wurde, wird mitgezählt. 
        //offene Felder + Markierte Felder = hoehe * breite -> dann ist das Spiel gewonnen.
        int anzahlOffenerFelder = 0;
        int zeitstempelStart;
        int zeitstempelGewonnen;
        int highscore = 99999;

        //die vom Benutzer eingegebene Anzahl von Bomben wird limitiert auf maxBombenAnzahl.
        //alles drunter wird akzeptiert.
        static int maxBombenAnzahl = breite * hoehe / 2;
        static int bombenAnzahl = maxBombenAnzahl;

        public Form1()
        {

            InitializeComponent();
            timer.Enabled = true;
            timer.Interval = 1000; //1000ms = 1 sek, die Uhr wird dann aktualisiert.
            timer.Tick += new EventHandler(Ticktack);
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            feldButton feld;
            for (int i = 0; i < breite * hoehe; i++)
            {
                feld = new feldButton();
                feld.index = i;
                feld.spalte = i % breite;
                feld.reihe = (i - feld.spalte) / breite;
                feld.Text = "";
                feld.UseVisualStyleBackColor = true;
                feld.Text = "";
                //einzelne Felder werden entsprechend "platziert".Jedes Feld ist 50 Pixel breit und hoch.
                feld.Location = new System.Drawing.Point((feld.spalte + 1) * 50, (feld.reihe + 1) * 50);
                feld.Size = new System.Drawing.Size(50, 50);
                feld.Visible = true;
                lstFelder.Add(feld);
                //die Felder werden zum "Leben erweckt". 
                this.Controls.Add(feld);
                //Hier wird die Methode "klicken" als Eventhandler zu jedem einzelnen Feld verknüft
                //damit die Klicks richtig zugeordnet werden können
                feld.MouseDown += (sender1, ex) => klicken(sender1, ex);
            }

            foreach (var f in lstFelder)
            {
                f.nachbarn = new List<feldButton>();

                for (int i = f.index - breite; i < f.index + breite + 1;)
                {
                    if (i - 1 >= 0 && i - 1 < breite * hoehe && Math.Abs(f.spalte - lstFelder[i - 1].spalte) < 2 )
                        f.nachbarn.Add(lstFelder[i - 1]);
                    if (i >= 0 && i < breite * hoehe && Math.Abs(f.spalte - lstFelder[i].spalte) < 2 && f.index != i)
                        f.nachbarn.Add(lstFelder[i]);
                    if (i + 1 >= 0 && i + 1 < breite * hoehe && Math.Abs(f.spalte - lstFelder[i + 1].spalte) < 2 )
                        f.nachbarn.Add(lstFelder[i + 1 ]);
                    i += breite;

                }

                //f.Text = "" + f.nachbarn.Count();
            }
           

            txtAnzahl.Text = "" + maxBombenAnzahl / 4;
                btnSmiley.Image = playingSmiley;

                //Button mit dem Smiley und Highscore usw. müssen auch abhängig von der Spielfeldgröße richtig platziert werden.
                grpInfo.Location = new System.Drawing.Point(breite * 50 + 65, 50);

                //Timer wird rechts unten platziert
                lblTimer.Location = new System.Drawing.Point(breite * 50 + 135, hoehe * 50 - 20);
                lblTimer.ForeColor = System.Drawing.Color.Red;
                lblTimer.BackColor = System.Drawing.Color.Black;
                neuSetzen();
        }

        //Diese Methode wird am Anfang 1 mal aufgerufen und dann auch jedes Mal wieder, wenn das Spiel neugestartet wird.
        void neuSetzen()
        {
            timer.Start();
            int temp;
            feldButton tempfeld;

            //bei "true" können die Buttons auch bedient werden.
            spielAktiv = true;
   
            bombenAnzahl = maxBombenAnzahl;

            //falls die angegebene/gewünschte Bombenanzahl zuviel ist, wird maxBombenAnzahl eingesetzt.
            if (Int32.TryParse(txtAnzahl.Text, out bombenAnzahl) &&
                    bombenAnzahl > maxBombenAnzahl) bombenAnzahl = maxBombenAnzahl;

            //Die Bombe wird erst als eine einfache Zufallszahl generiert.
            // bei einem 10 x 10 Spielfeld werden Zufallszahlen zwischen 1 und 100 erstellt
            // bei 20 x 20 dementsprechend zwischen 1 und 400.
            int randomLimit = breite * hoehe; //
            int bombX, bombY;
            List<int> bombenNummern = new List<int>();
            bomben = new List<feldButton>();
            btnSmiley.Image = playingSmiley;

            //Hier zum Spielneustart muss der Zähler zurückgesetzt werden.
            flagCounter = 0;
            lblAnzahlmarkiert.Text = "0";
            anzahlOffenerFelder = 0;

            //damit man die verstrichene Zeit rausbekommen kann muss der Zeitstempel gespeichert werden.
            zeitstempelStart = (DateTime.Now.Hour * 60 + DateTime.Now.Minute) * 60 + DateTime.Now.Second;
            //leere Felder erzeugen.

            foreach (var feld in lstFelder)
            {
                feld.Image = unflagged; //leere Felder 
                feld.wert = 0; //bedeutet keine Bombe & kein Wert -> keine Bombe bei einem der Nachbarn
                feld.flagged = false; //zum Spielstart Flaggen löschen
                feld.questionMarked = false; //Fragezeichen ebenfalls löschen
                feld.offen = false; //offene Felder zurücksetzen.


            }
     
            //Bomben legen
            for (int i = 0; i < bombenAnzahl; i++)
            {
                //wenn die generierte Zufallszahl schon vorhanden ist dann wiederholen.
                do
                {
                    temp = rnd.Next(1, lstFelder.Count());
                } while (istDoppelt(bombenNummern, temp));

                //die liste wird benötigt, damit doppelte Zahlen ausgeschlossen werden können.
                bombenNummern.Add(temp);

                tempfeld = lstFelder[temp-1];
                //ein Feld mit einer Bombe erhält den Wert 9
                tempfeld.wert = 9;
                bomben.Add(tempfeld);
                //Nachbarn informieren
                //Also der wert wird bei jedem Nachbar erhöht, die selbst keine Bombe besitzt.
                //somit werden hier die Felder mit Zahlenwerten erstellt.
                foreach (var feld in tempfeld.nachbarn)
                {
                    if (feld.wert != 9) feld.wert++;
                }
            }

        }

        //Die Methode gibt die Aktuelle Uhrzeit in [Sekunden] zurück
        private int holeZeitstempel()
        {
            return (DateTime.Now.Hour * 60 + DateTime.Now.Minute) * 60 + DateTime.Now.Second;
        }

        //Hier wird die Spielzeit aktualisiert.
        private void Ticktack(object sender, EventArgs e)
        {
            int zeitVerstrichen = holeZeitstempel() - zeitstempelStart;

            if (zeitVerstrichen < 10) lblTimer.Text = "00" + zeitVerstrichen;
            else 
            if (zeitVerstrichen < 100 && zeitVerstrichen > 9) lblTimer.Text = "0" + zeitVerstrichen;
            else lblTimer.Text = "" + zeitVerstrichen;

        }

        //Diese Methode wird jedesmal als Eventhandler aufgerufen, wenn ein Button bzw. Feld bedient wurde.
        void klicken(object sender, MouseEventArgs e)
        {
            //Wenn das Spiel gewonnen oder verloren ist dann wird hier nix weiter gemacht.
            if (!spielAktiv) return; 
            feldButton btn = sender as feldButton;
            if (e.Button == MouseButtons.Left)
                    if (btn.offen) 
                        feldChecken(btn); //wenn die linke Maustaste auf einem bereits offenen Feld gedrückt wird, wird geprüft ob was gemacht werden muss.
                else oeffnen(btn); //wenn das Feld noch nicht offen ist dann öffnen
                else if (e.Button == MouseButtons.Right && !btn.offen)
                { //wenn button nicht offen ist und Rechts klick betätigt wird, und nicht markiert ist dann mit der Flagge markieren.
                    if (!btn.flagged && !btn.questionMarked)
                        flaggeSetzen(btn);
                    else
                        if (btn.flagged) fragezeichenSetzen(btn); //wenn schon eine Flagge gesetzt ist dann kommt ein Fragezeichen
                    else fragezeichenEntfernen(btn); //wenn bereits eine Fragezeichen da ist, dann wird das Feld wieder leer gemacht.
                }
        }

        //Ein einzelnes Feld wird mit der Flagge markiert
        void flaggeSetzen(feldButton b)
        {
            b.Image = flagged;
            b.flagged = true;

            // Flagge muss gezählt werden, damit das "Gewinnen" erkennt werden kann.
            flagCounter++;
            lblAnzahlmarkiert.Text = "" + flagCounter; //Der Benutzer wird auch darüber informiert, wieviel Flaggen er schon gesetzt hat.


            // DAs Spiel ist gewonnen, wenn alle Bomben markiert sind und sonst andere Felder alle offen sind.
            if (anzahlOffenerFelder + flagCounter == breite * hoehe && bombenAnzahl == flagCounter)
            {
                gewonnen();
            }
        }

        void fragezeichenSetzen(feldButton b)
        {
            b.Image = questionMark;
            b.questionMarked = true;
            b.flagged = false;
            flagCounter--; //den Counter nicht vergessen ;-) Keine Flagge -> keine Markierung
            lblAnzahlmarkiert.Text = "" + flagCounter;
        }

        void fragezeichenEntfernen(feldButton b)
        {
            b.Image = unflagged;
            b.questionMarked = false;
        }

        //Die Methode wird rekursiv aufgerufen, wenn ein "leeres" Feld geöffnet wird.
        //Darum ist es wichtig gleich am Anfang zu prüfen ob das Feld bereits offen ist.
        //Dann wenn es offen ist wird die Rekursion beendet, damit keine Endlose Rekursion entsteht.
        //Wenn das Feld eine Markierung hat dann auch nichts unternehmen.
        void oeffnen(feldButton b)
        {
            if (b.offen == true)
                return;

            if (b.flagged)
                return;

            b.offen = true;
            anzahlOffenerFelder++;

            if (b.wert == 0) //wenn das Feld leer -> dann alle Nachbarn auch öffnen, da keine Bombengefahr besteht.
                foreach (feldButton fb in b.nachbarn)
                    oeffnen(fb); 


            //Hier wird nach dem Öffnen, das richtige Bild auf dem Button geladen.
            //Hier bei case 9 wird dann erkannt, wenn ein Feld mit Bombe geöffnet wurde. -> Spielende
            switch (b.wert)
            {
                case 0:
                    b.Image = null;
                    break;
                case 1:
                    b.Image = wert1;
                    break;
                case 2:
                    b.Image = wert2;
                    break;
                case 3:
                    b.Image = wert3;
                    break;
                case 4:
                    b.Image = wert4;
                    break;
                case 5:
                    b.Image = wert5;
                    break;
                case 6:
                    b.Image = wert6;
                    break;
                case 7:
                    b.Image = wert7;
                    break;
                case 8:
                    b.Image = wert8;
                    break;
                case 9:
                    b.Image = blast;
                    //Die Bombe ist explodiert und mit rot markiert. darum aus der Liste raus, 
                    //so dass sie immer noch rot markiert bleibt, wenn die anderen Bomben aufgedeckt werden
                    bomben.Remove(b); 
                    verloren();
                    break;
                default:
                    break;
            }

            //Hier muss selbstverständlich auch geprüft werden, ob es alles war.
            if (anzahlOffenerFelder + flagCounter == breite * hoehe && bombenAnzahl == flagCounter )
            {
                gewonnen();
            }
        }

        //Die Methode wird dann aufgerufen, wenn ein offener Feld mit einem Zahlenwert geklickt wird,
        //damit die Nachbarfelder auf Wunsch des Benutzers geöffnet werden.
        private void feldChecken(feldButton b)
        {
            //wenn das geklickte Feld leer ist, dann wird nix unternommen.
            if (b.wert == 0) 
                return;
            int anzahlMarkierteBomben = 0;
            int anzahlMarkierungen = 0;

            //wenn das Feld ein Wert hat, dann kontrollieren ob die Bomben in der Nachbarschaft richtig markiert wurden.
            foreach (feldButton fb in b.nachbarn)
                if (fb.flagged)
                {
                    anzahlMarkierungen++;
                    if (fb.wert == 9)
                        anzahlMarkierteBomben++;
                }

            // wenn die Anzahl mit den Markierungen und Bombenanzahl passt und richtig markiert sind, dann die Nachbarn öffnen.
            if (b.wert == anzahlMarkierteBomben && b.wert == anzahlMarkierungen)
            {
                foreach (feldButton fb in b.nachbarn)
                {   //der Nachbar wird auch nur dann geöffnet, wenn er nicht bereits offen ist.
                    if (!fb.offen) oeffnen(fb);
                }
                return;
            }
            //wenn die richtige Anzahl Bomben markiert sind aber nicht korrekt, dann beendet das Spiel
            //anzahlMarkierteBomben = richtig markierte Bomben.
            //Wenn die Markierungen nicht die Zahl entspricht, dann nichts unternehmen
            //da es sich dann um ein Versehen handelt.
            if (anzahlMarkierungen == b.wert && anzahlMarkierteBomben != b.wert)
                verloren();

        }

        //Die Methode wird beim Erstellen der Bombe aufgerufen
        //Dafür wurde die Liste oben aufgeführt, damit hierher zur Prüfung übergeben werden konnte.
        bool istDoppelt(IList<int> ilist, int wert)
        {
            for (int i = 0; i < ilist.Count; i++)
                if (ilist[i] == wert)
                    return true;
            return false;
        }

        //Wenn Spiel verloren ist, dann alle Bomben aufdecken, timer stop und spielAktive = false bedeutet
        //auf dem Spielfeld kann man nichts mehr bedienen.
        void verloren()
        {
            spielAktiv = false;
            btnSmiley.Image = looseSmiley;
            foreach (feldButton b in bomben)
                b.Image = bomb;
            timer.Stop();


        }

        //Hier wird auch das Spielfeld gesperrt.
        //für Highscore wird die Zeit auch geholt.
        //wurde Highscore unterboten, dann auch dementsprechend die Anzeige ändern
        void gewonnen()
        {
            btnSmiley.Image = woonSmiley;
            spielAktiv = false;
            zeitstempelGewonnen = holeZeitstempel()-zeitstempelStart;
            if (zeitstempelGewonnen < highscore)
            {
                highscore = zeitstempelGewonnen;
                lblHighscoreScore.Text = lblTimer.Text;
            }
            timer.Stop();

        }
        private void lblAusgabe_Click(object sender, EventArgs e)
        {

        }

        //Das Spiel wird durch die Smileytaste neugestartet
        private void btnSmiley_Click(object sender, EventArgs e)
        {
            neuSetzen();
        }

        private void btnSmiley_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtAnzahl_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblHighscoreScore_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

    //Die Grundidee der Realisierung, wenn man für die einzelnen Felder Buttons einsetzen möchte.
    //Ohne diese Vererbung wäre die Zuordnung mit den Werten, Markierungen und Nachbarn schwierig
    //bzw. sehr aufwendig.
    class feldButton : Button
    {
        public int wert, spalte, reihe, index;
        public Boolean offen = false;
        public Boolean flagged = false;
        public Boolean questionMarked = false;

        public List<feldButton> nachbarn = new List<feldButton>();
    }

}
