using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeCryptography : ImpostorMod
{
    public override string ModAbbreviation { get { return "Cy"; } }
    public TextMesh[] buttonLetters;
    public Text display;
    private string formattedText;
    private static readonly string wholeExcerpt = "Marley estaba muerto; eso para empezar. No cabe la menor duda al respecto. El clerigo, el funcionario, el propietario de la funeraria y el que presidio el duelo habian firmado el acta de su enterramiento. Tambien Scrooge habia firmado, y la firma de Scrooge, de reconocida solvencia en el mundo mercantil, tenia valor en cualquier papel donde apareciera. El viejo Morley estaba tan muerto como el clavo de una puerta. !Atencion! No pretendo decir que yo sepa lo que hay de especialmente muerto en el clavo de una puerta. Yo, mas bien, me habia inclinado a considerar el clavo de un ataud como el mas muerto de todos los articulos de ferreteria. Pero en el simil se contiene el buen juicio de nuestros ancestros, y no seran mis manos impias las que lo alteren. Por consiguiente, permitaseme repetir enfaticamente que Marley es taba tan muerto como el clavo de una puerta. ?Sabia Scrooge que estaba muerto? Claro que si. ?Como no iba a saberlo? Scrooge y el habian sido socios durante no se cuantos anos. Scrooge fue su unico albacea testamentario, su unico administrador, su unico asignatario, su unico heredero residual, su unico amigo y el unico que llevo luto por el. Y ni siquiera Scrooge quedo terriblemente afectado por el luctuoso suceso; siguio siendo un excelente hombre de negocios el mismisimo dia del funeral, que fue solemnizado por el a precio de ganga. La mencion del funeral de Marley me hace retroceder al punto en que empece. No cabe duda de que Marley estaba muerto. Es preciso comprenderlo con toda claridad, pues de otro modo no habria nada prodigioso en la historia que voy a relatar. Si no estuviesemos co mpletamente convencidos de que el padre de Hamlet ya habia fallecido antes de levantarse el telon, no habria nada notable en sus paseos nocturnos por las murallas de su propiedad, con viento del Este, como para causar asombro -en sentido literal- en la mente enfermiza de su hijo; seria como si cualquier otro caballero de mediana edad saliese irreflexivamente tras la caida de la noche a un lugar oreado, por ejemplo, el camposanto de Saint Paul. Scrooge nunca tacho el nombre del viejo Marley. Anos despues, alli seguia sobre la entrada del almacen: <<Scrooge y Marley>>. La firma comercial era conocida por <<Scrooge y Marley>>. Algunas personas, nuevas en el negocio, algunas veces llamaban a Scrooge, <<Scrooge>>, y otras, <<Marley>>, pero el atendia por los dos nombres; le daba lo mismo. !Ay, pero que agarrado era aquel Scrooge! !Viejo pecador avariento que extorsionaba, tergiversaba, usurpaba, rebanaba, apresaba! Duro y agudo como un pedemal al que ningun eslabon logro jamas sacar una chispa de generosidad; era secreto, reprimido y solitario como una ostra. La frialdad que tenia dentro habia congelado sus viejas facciones y afilaba su nariz puntiaguda, acartonaba sus mejillas, daba rigidez a su porte; habia enrojecido sus ojos, azulado sus finos labios; esa frialdad se percibia claramente en su voz raspante. Habia escarcha canosa en su cabeza, cejas y tenso menton. Siempre llevaba consigo su gelida temperatura; el hacia que su despacho estuviese helado en los dias mas calurosos del verano, y en Navidad no se deshelaba ni un grado. Poco influian en Scrooge el frio y el calor externos. Ninguna fuente de calor podria calentarle, ningun frio invernal escalofriarle. El era mas cortante que cualquier viento, mas pertinaz que cualquier nevada, mas insensible a las suplicas que la lluvia torrencial. Las inclemencias del tiempo no podian superarle. Las peores lluvias, nevadas, granizadas y neviscas podrian presumir de sacarle ventaja en un aspecto: a menudo ellas <<se desprendian>> con generosidad, cosa que Scrooge nunca hacia. Jamas le paraba nadie en la calle para decirle con alegre semblante: <<Mi querido Scrooge, ?como esta usted? ?Cuando vendra a visitarme?>> Ningun mendigo le pedia limosna; ningun nino le preguntaba la hora; ningun hombre o mujer le habia preguntado por una direccion ni una sola vez en su vida. Hasta los perros de los ciegos parecian conocerle; al verle acercarse, arrastraban precipitadamente a sus duenos hasta los portales y los patios, y despues daban el rabo, como diciendo: <<!Es mejor no tener ojo que tener el mal de ojo, amo ciego!>> Pero a Scrooge, ?que le importaba? Eso era preicsamente lo que le gustaba. Para el era una <<gozada>> abrirse camino entre los atestados senderos de la vida advirtiendo a todo sentimiento de simpatia humana que guardase las distancias.";


    void Start()
    {

        var clauses = wholeExcerpt.Split(new[] { '.', '?', '!', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Where(x => !x.All(ch => char.IsWhiteSpace(ch)));
        var pickedWords = clauses.PickRandom().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(
                                                    word => word.Where(
                                                            ch => char.IsLetter(ch)
                                                                ).Join("").Trim().ToUpper());
        string[] formattedWords = pickedWords.Select(w => string.Format("<color=#{0}>{1}</color>", GetColor(w), w)).ToArray();
        formattedText = formattedWords.Join(" ").Trim();
        flickerObjs.Add(display.gameObject);
        LogQuirk("the text is in Spanish");

        char[] availableAlphabet = pickedWords.SelectMany(x => x).Where(ch => char.IsLetter(ch)).Distinct().ToArray().Shuffle();
        for (int i = 0; i < 5; i++)
            buttonLetters[i].text = availableAlphabet[i].ToString();
    }
    public override void OnActivate()
    {
        display.text = formattedText;
    }
    string GetColor(string word)
    {
        word = word.ToUpperInvariant();
        if (word.CountOf('T') >= 2)
            return "65B86F"; //Green
        else if (word.Count(x => "AEIOU".Contains(x)) == 1)
            return "FBFEB1"; //Yellow
        else
            return "E5B0AD"; //Pink
    }
}
