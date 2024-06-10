/* global use, db */
// MongoDB Playground
// To disable this template go to Settings | MongoDB | Use Default Template For Playground.
// Make sure you are connected to enable completions and to be able to run a playground.
// Use Ctrl+Space inside a snippet or a string literal to trigger completions.
// The result of the last command run in a playground is shown on the results panel.
// By default the first 20 documents will be returned with a cursor.
// Use 'console.log()' to print to the debug output.
// For more documentation on playgrounds please refer to
// https://www.mongodb.com/docs/mongodb-vscode/playgrounds/

// Select the database to use.
use('carotte');

const collection = 'Quotes';

db.createCollection(collection)
db.Quotes.insertMany(
  [
    { "label": "Le seul endroit où le succès vient avant le travail, c’est dans le dictionnaire. - Vidal Sassoon" },
    { "label": "Ne regardez pas en arrière avec colère ou en avant avec peur, mais autour de vous avec conscience. - James Thurber" },
    { "label": "Il ne faut pas attendre d’être parfait pour commencer quelque chose de bien. - Abbé Pierre" },
    { "label": "Le succès, c'est se promener d’échec en échec tout en restant motivé. - Winston Churchill" },
    { "label": "Vous devez être le changement que vous voulez voir dans ce monde. - Mahatma Gandhi" },
    { "label": "La vie, c’est comme une bicyclette, il faut avancer pour ne pas perdre l’équilibre. - Albert Einstein" },
    { "label": "L’obstination est le chemin de la réussite. - Charlie Chaplin" },
    { "label": "Les grandes réalisations sont toujours précédées par de grandes idées. - Steve Jobs" },
    { "label": "Le bonheur n’est pas quelque chose de tout fait. Il vient de vos propres actions. - Dalai Lama" },
    { "label": "Le plus grand risque est de ne jamais prendre de risques. - Mark Zuckerberg" },
    { "label": "Croyez en vous et en tout ce que vous êtes. Sachez qu'il y a quelque chose à l'intérieur de vous qui est plus grand que n'importe quel obstacle. - Christian D. Larson" },
    { "label": "Le succès n'est pas la clé du bonheur. Le bonheur est la clé du succès. Si vous aimez ce que vous faites, vous réussirez. - Albert Schweitzer" },
    { "label": "Ne rêvez pas de gagner, entraînez-vous. - Mohammad Ali" },
    { "label": "Ce n'est pas la volonté de gagner qui compte, tout le monde l'a. C'est la volonté de se préparer à gagner qui compte. - Paul Bryant" },
    { "label": "Les seules limites de nos réalisations de demain sont nos doutes d’aujourd’hui. - Franklin D. Roosevelt" },
    { "label": "Le seul moyen de faire du bon travail est d’aimer ce que vous faites. - Steve Jobs" },
    { "label": "Les champions ne se forgent pas dans les salles de sport. Les champions se forgent à partir de quelque chose qu'ils ont profondément en eux : un désir, un rêve, une vision. - Muhammad Ali" },
    { "label": "La meilleure façon de prédire l'avenir est de le créer. - Peter Drucker" },
    { "label": "La vie est 10 % ce qui nous arrive et 90 % comment nous réagissons. - Charles R. Swindoll" },
    { "label": "Pour réussir, votre désir de succès doit être plus grand que votre peur de l’échec. - Bill Cosby" },
    { "label": "La seule limite à notre épanouissement de demain sera nos doutes d’aujourd’hui. - Franklin D. Roosevelt" },
    { "label": "Le succès n'est pas final, l'échec n'est pas fatal : c'est le courage de continuer qui compte. - Winston Churchill" },
    { "label": "La motivation vous sert de départ. L’habitude vous fait continuer. - Jim Ryun" },
    { "label": "La réussite, c'est tomber sept fois, se relever huit. - Proverbe japonais" },
    { "label": "Ne jugez pas chaque jour à la récolte que vous faites mais aux graines que vous semez. - Robert Louis Stevenson" },
    { "label": "Il n’y a pas d’ascenseur vers le succès, vous devez prendre les escaliers. - Zig Ziglar" },
    { "label": "Le succès, c’est la somme de petits efforts, répétés jour après jour. - Leo Robert Collier" },
    { "label": "Ne laissez jamais vos peurs décider de votre avenir. - Anonyme" },
    { "label": "Rêvez grand et osez échouer. - Norman Vaughan" },
    { "label": "L’avenir appartient à ceux qui croient à la beauté de leurs rêves. - Eleanor Roosevelt" },
    { "label": "Commencez là où vous êtes, utilisez ce que vous avez, faites ce que vous pouvez. - Arthur Ashe" },
    { "label": "Ce n'est pas la force, mais la persévérance, qui fait les grandes œuvres. - Samuel Johnson" },
    { "label": "La plus grande gloire n’est pas de ne jamais tomber, mais de se relever à chaque chute. - Confucius" },
    { "label": "Si vous ne pouvez pas voler, alors courez ; si vous ne pouvez pas courir, alors marchez ; si vous ne pouvez pas marcher, alors rampez, mais quoi que vous fassiez, vous devez continuer d’avancer. - Martin Luther King Jr." },
    { "label": "Il n’est jamais trop tard pour devenir ce que vous auriez pu être. - George Eliot" },
    { "label": "Pour réaliser de grandes choses, il ne suffit pas d’agir, il faut rêver ; il ne suffit pas de rêver, il faut agir. - Anatole France" },
    { "label": "Votre temps est limité, ne le gâchez pas en menant une existence qui n’est pas la vôtre. - Steve Jobs" },
    { "label": "La vie commence à la fin de votre zone de confort. - Neale Donald Walsch" },
    { "label": "Ne vous arrêtez pas quand vous êtes fatigué. Arrêtez-vous quand vous avez terminé. - Marilyn Monroe" },
    { "label": "Le succès est la somme de petits efforts, répétés jour après jour. - Robert Collier" },
    { "label": "Le futur appartient à ceux qui croient en la beauté de leurs rêves. - Eleanor Roosevelt" },
    { "label": "La seule chose que nous devons craindre est la peur elle-même. - Franklin D. Roosevelt" },
    { "label": "N'abandonne jamais, car c'est exactement le lieu et le moment où la marée changera. - Harriet Beecher Stowe" },
    { "label": "La plus grande aventure que vous pouvez entreprendre est de vivre la vie de vos rêves. - Oprah Winfrey" },
    { "label": "Le secret du changement consiste à concentrer son énergie pour créer du nouveau, et non pour se battre contre l'ancien. - Dan Millman" },
    { "label": "Croyez que vous pouvez et vous serez à mi-chemin. - Theodore Roosevelt" },
    { "label": "Le succès est un voyage, pas une destination. L'action est souvent plus importante que le résultat. - Arthur Ashe" },
    { "label": "L'échec est le fondement de la réussite. - Lao Tseu" },
    { "label": "Vous ne trouverez jamais ce que vous ne cherchez pas. - Confucius" },
    { "label": "Ne comptez pas les jours, faites que les jours comptent. - Muhammad Ali" },
    { "label": "Chaque moment est un nouveau départ. - T.S. Eliot" },
    { "label": "Les grandes choses ne se produisent jamais dans la zone de confort. - Neil Strauss" },
    { "label": "La seule limite à la hauteur de vos réalisations est la portée de vos rêves et votre volonté de travailler pour les réaliser. - Michelle Obama" },
    { "label": "Le futur appartient à ceux qui voient les possibilités avant qu'elles ne deviennent évidentes. - John Sculley" },
    { "label": "Il est toujours trop tôt pour abandonner. - Norman Vincent Peale" },
    { "label": "La vie, ce n'est pas d'attendre que l'orage passe, c'est d'apprendre à danser sous la pluie. - Sénèque" },
    { "label": "Ce n'est pas le plus fort de l'espèce qui survit, ni le plus intelligent, mais celui qui est le plus réactif au changement. - Charles Darwin" },
    { "label": "Votre avenir est créé par ce que vous faites aujourd'hui, pas demain. - Robert Kiyosaki" },
    { "label": "Ne regardez pas l'horloge ; faites comme elle, avancez. - Sam Levenson" },
    { "label": "Vous êtes plus fort que vous ne le croyez. Vous avez plus de talents que vous ne l'imaginez. - Roy T. Bennett" },
    { "label": "La meilleure façon de commencer quelque chose est de cesser de parler et de commencer à faire. - Walt Disney" },
    { "label": "Chaque grande réalisation commence par la décision d'essayer. - Gail Devers" },
    { "label": "Croyez en vous-même et en tout ce que vous êtes. Sachez qu'il y a quelque chose en vous qui est plus grand que n'importe quel obstacle. - Christian D. Larson" },
    { "label": "Faites ce que vous pouvez, avec ce que vous avez, là où vous êtes. - Theodore Roosevelt" },
    { "label": "Les rêves ne deviennent réalité que lorsque nous les poursuivons. - John C. Maxwell" },
    { "label": "Le courage ne rugit pas toujours. Parfois, le courage est la petite voix tranquille à la fin de la journée qui dit : 'Je recommencerai demain.' - Mary Anne Radmacher" },
    { "label": "La distance entre vos rêves et la réalité s'appelle l'action. - Anonyme" },
    { "label": "Le seul moyen de faire un excellent travail est d'aimer ce que vous faites. Si vous n'avez pas encore trouvé, continuez à chercher. Ne vous contentez pas. - Steve Jobs" },
    { "label": "Le bonheur est la clé du succès. Si vous aimez ce que vous faites, vous réussirez. - Albert Schweitzer" },
    { "label": "N'attendez pas pour frapper le fer quand il est chaud, mais rendez-le chaud en le frappant. - William Butler Yeats" },
    { "label": "Les obstacles sont ces choses effrayantes que vous voyez lorsque vous détournez les yeux de votre objectif. - Henry Ford" },
    { "label": "La force ne vient pas de la capacité physique, elle vient d'une volonté indomptable. - Mahatma Gandhi" },
    { "label": "La plus grande gloire n'est pas de ne jamais tomber, mais de se relever à chaque chute. - Nelson Mandela" },
    { "label": "Ce n'est pas ce que vous regardez qui compte, c'est ce que vous voyez. - Henry David Thoreau" },
    { "label": "Le secret pour aller de l'avant est de commencer. - Mark Twain" },
    { "label": "La vie est ce que vous en faites. Alors, continuez à la rendre géniale. - Maya Angelou" },
    { "label": "Le seul endroit où le succès précède le travail est dans le dictionnaire. - Vidal Sassoon" }
  ]
);