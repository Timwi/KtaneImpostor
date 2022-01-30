using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeOnlyConnect : ImpostorMod
{
    public override string ModAbbreviation { get { return "Oc"; } }
    public MeshRenderer[] buttonObjs;
    public Texture[] hieroglyphTextures;
    public Texture susTexture;
    public TextMesh teamText;

    private static readonly string[] teamNames = { "ACCOUNTANTS", "ANCIENT ALUMNI", "ANIMAL LOVERS", "APOLLOS", "APRES SKIERS", "ARCHERS ADMIRERS", "ARROWHEADS", "ATHENIANS", "BAKERS", "BARDOPHILES", "BARONS", "BEAKS", "BEEKEEPERS", "BELGOPHILES", "BIBLIOPHILES", "BIRDWATCHERS", "BOARD GAMERS", "BOOKSELLERS", "BOOKWORMS", "BOWLERS", "BRASENOSE POSTGRADS", "BREWS", "BRIDGES", "BUILDERS", "CAMBRIDGE QUIZ SOCIETY", "CARTOONISTS", "CARTOPHILES", "CAT LOVERS", "CELTS", "CHANNEL ISLANDERS", "CHARITY PUZZLERS", "CHESS PIECES", "CHESSMEN", "CHOIR BOYS", "CHORISTERS", "CINEPHILES", "CIPHERS", "CLAREITES", "CLUESMITHS", "CODERS", "COLLEAGUES", "COLLECTORS", "CORKSCREWS", "CORPUSCLES", "COSMOPOLITANS", "COUSINS", "CRICKETERS", "CROSSWORDERS", "CURIOSITIES", "DANDIES", "DARKSIDERS", "DATA WIZARDS", "DEBUGGERS", "DETECTIVES", "DICERS", "DISCOTHEQUES", "DISPARATES", "DOUBLE-OH SEVENS", "DRAGONS", "DRAUGHTSMEN", "DUNGEON MASTERS", "DURHAMITES", "ECO-WARRIORS", "EDUCATORS", "EDWARDS FAMILY", "EGGCHASERS", "ELECTROPHILES", "ENDEAVOURS", "ERSTWHILE ATHLETES", "ESCAPOLOGISTS", "EUROPHILES", "EUROVISIONARIES", "EXETER ALUMNI", "EXHIBITIONISTS", "EXTRAS", "FELINOPHILES", "FELL WALKERS", "FESTIVAL FANS", "FIRE-EATERS", "FOOTBALLERS", "FORRESTS", "FRANCOPHILES", "GAFFERS", "GALLIFREYANS", "GAMBLERS", "GAMEMAKERS", "GAMESMASTERS", "GENEALOGISTS", "GENERAL PRACTITIONERS", "GEOCACHERS", "GLADIATORS", "GLOBETROTTERS", "GODYN FAMILY", "GOLFERS", "GOURMANDS", "HEADLINERS", "HEATH FAMILY", "HIGHGATES", "HISTORY BOYS", "HITCHHIKERS", "HOTPOTS", "INQUISITORS", "INSURERS", "IT SPECIALISTS", "JOINEES", "JOURNEYMEN", "JUKEBOXERS", "JUNIPERS", "KORFBALLERS", "LAPSED PHYSICISTS", "LARPERS", "LASLETTS", "LEXPLORERS", "LIBRARIANS", "LINGUISTS", "MALTSTERS", "MATHEMATICIANS", "MEDICS", "MEEPLES", "MIXOLOGISTS", "MOTORHEADS", "MUPPETS", "MUSIC LOVERS", "MUSIC MONKEYS", "NETWORKERS", "NEUROSCIENTISTS", "NIGHTWATCHMEN", "NOGGINS", "NORDIPHILES", "NUMERISTS", "OENOPHILES", "OPERATIONAL RESEARCHERS", "ORIENTEERS", "ORWELLIANS", "OSCAR MEN", "OUTLIERS", "OXFORD LIBRARIANS", "OXONIANS", "PARISHIONERS", "PART-TIME POETS", "PEDAGOGUES", "PHILOSOPHERS", "PILGRIMS", "PILOTS", "POLICY WONKS", "POLITICOS", "POLYGLOTS", "POLYHYMNIANS", "POLYMATHS", "POPTIMISTS", "PRESS GANG", "PSMITHS", "PUZZLE HUNTERS", "PYROMANIACS", "QI ELVES", "QUITTERS", "RAILWAYMEN", "RAMBLERS", "RECORD COLLECTORS", "RELATIVES", "ROAD TRIPPERS", "ROMANTICS", "RUGBY BOYS", "RUGBY FANS", "SANDY SHORES", "SCIENCE EDITORS", "SCIENTISTS", "SCRABBLERS", "SCRIBBLERS", "SCRIBES", "SCRUBS", "SCUNTHORPE SCHOLARS", "SECOND VIOLINISTS", "SEVERNS", "SHUTTERBUGS", "SLIDERS", "SNAKE CHARMERS", "SOFTWARE ENGINEERS", "SPAGHETTI WESTERNERS", "STEELERS", "STEWARDS", "STRATEGISTS", "STRING SECTION", "SUITS", "SURREALISTS", "TAVERNERS", "TEFL TEACHERS", "TEQUILA SLAMMERS", "TERRIERS", "THE BALDING TEAM", "THE WRIGHTS", "THEATRICALS", "THREE PEAKS", "TICKET COLLECTORS", "TILLERS", "TIME LADIES", "TRENCHERMEN", "TUBERS", "TUROPHILES", "URBAN WALKERS", "VERBIVORES", "VIKINGS", "WALRUSES", "WANDERERS", "WANDERING MINSTRELS", "WATERBABIES", "WAYFARERS", "WELSH LEARNERS", "WESTENDERS", "WHITCOMBES", "WHODUNNITS", "WICKETS", "WILDLIFERS", "WINTONIANS", "WOMBLES", "WOOLGATHERERS", "WORDSMITHS", "WRESTLERS", "WRIGHTS", "YORKERS" };
    private int Case;
    
    void Start()
    {
        var rndSynbols = Enumerable.Range(0, 6).ToArray().Shuffle();
        for (int i = 0; i < buttonObjs.Length; i++)
            buttonObjs[i].material.mainTexture = hieroglyphTextures[rndSynbols[i]];
        teamText.text = teamNames.PickRandom();
        Case = Rnd.Range(0, 2);
        switch (Case)
        {
            case 0:
                int rnd1, rnd2;
                do
                {
                    rnd1 = Rnd.Range(0, 6);
                    rnd2 = Rnd.Range(0, 6);
                } while (rnd1 == rnd2);
                var shuff = Enumerable.Range(0, 6).ToArray().Shuffle();
                shuff[rnd1] = shuff[rnd2];
                flickerObjs.Add(buttonObjs[rnd1].gameObject);
                flickerObjs.Add(buttonObjs[rnd2].gameObject);
                for (int i = 0; i < buttonObjs.Length; i++)
                    buttonObjs[i].material.mainTexture = hieroglyphTextures[shuff[i]];
                Log("there are duplicate hieroglyphs");
                break;
            case 1:
                var sus = Rnd.Range(0, 6);
                buttonObjs[sus].material.mainTexture = susTexture;
                flickerObjs.Add(buttonObjs[sus].gameObject);
                Log("there is a unusually suspicious hieroglyph");
                break;
        }
    }
}
