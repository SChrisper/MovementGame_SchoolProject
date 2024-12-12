# MovementGame_SchoolProject

Tervetuloa MovementSimulaattoriin

Tässä ReadMe filessä kerron kaikesta, mitä sinun pitäisi tietää tästä pelistä!


Basic liikkuminen
-----------------

W,A,S,D - perus liikuminen eteenpäin, taakse, oikealle ja vasemmalle

SPACEBAR - hyppy

SHIFT - Juoksu


Complicated liikkuminen
-----------------
Q - Syöksyminen, syöksy eteenpäin korkealla nopeudella

Left Mouse Button - Swinging, jos tarpeeksi lähellä seinää, kiinnity siihen köydellä ja heilu haluamaasi suuntaan

Right Mouse Button - Grapple, Painamalla right clickiä pystyt kiinnittyä objektiin köydellä ja alat lentämään siihen suuntaan.

Seinä + W - Jos olet juoksemassa eteenpäin seinää vasten ilmassa, alat juoksemaan seinän päällä, jonka jälkeen voit painaa mitä nappia tahansa lopettaakseen seinäjuokseminen.

Synergies (mitkä liikeet toimivat hyvin toisiinsa kanssa)
-----------------
Painamalla Q samalla kuin heilut, niin pelaajaa heitetään korkealle ja antaa pelaajalle hyvän nopeuden.

info
-----------------
Tässä Pelissä sinun tavoite on päästä maaliin niin nopeasti, kuin pystyt.
Maali on iso vihreä läpinäkyvä kuutio, johon heti kuin kosket sitä, heittää sut suoraan tulostaululle, johon pystyt laittaa oman nimen ja se tallentaa sen lokaaliin SQL tietokantaan.

Ruudun vasemmalla yläpuolella on ajastin, joka alkaa laskemaan heti, kun alat liikkumaan ja heti kun pääset tulostaululle, sun aika muutetaa pisteiksi (esim. Jos aikasi olisi 1:27.43, se sit poistaa kaikki ":" ja ".", jonka jälkeen se näyttäisi tältä "12743")
Nimi ja pisteet tallentuu tietokantaan, joka löytyy "C:\Users\ "Käyttäjän nimi" \AppData\LocalLow\DefaultCompany\MovementSimulator\HighScore.db"

käyttämällä esim. SQLiteStudiota pystyt lisäämään, muokkaamaan ja poistamaan tiedot.

Mappi on randomisoitu käyttäen Perlin Noise Math Funktiota, joka antaa pelaajalle enemmän haasteita saamaan pienemmän ajan.



Origins
-----------------

Sain idean tehdä tämä peli, kuin katsoin minkälaisia peleja pelaan ja minkälaisia sarjoja katson (esim. ULTRAKILL ja Attack on Titan)

Tästä pelistä oli vanhempi versio, jossa oli vähemmän asioita, ja olin vahingossa poistanu ison scriptin, jonka jälkeen päätin tehdä kokonaan uuden pelin.
Vanhan pelin nimi oli ODMSimulaattor, ODM on Attack on Titan sarjasta ihmisten kehittämiä laitteita, jotka mahdollistavat suuren ja tarkan liikkuvuuden kohdatessaan titaaneja taistelussa.

Nykyisessä projektissa on paljon enemmän asioita kuin alkuperäisessä pelissä, kuten syöksyminen, heittokoukkuase ja seinien pällä juokseminen.

Aloitin tän pelin teko alku marraskuussa ja se eteni suht. hyvin deadlinea asti (10.12.2024).
