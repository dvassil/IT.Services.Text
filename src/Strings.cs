﻿namespace IT.Services.Text
{
    internal static class Strings
    {
        public static string[] d      = { "", "δεκα", "είκοσι ", "τριάντα ", "σαράντα ", "πενήντα ", "εξήντα ", "εβδομήντα ", "ογδόντα ", "ενενήντα " };
        public static string[] idx =    { "¢", "€", "χιλιάδες ", "εκατομμύρι", "δισ", "τρισ", "τετρά", "πεντά", "εξά", "επτά", "οκτά", "εννιά", "δεκά", "ένδεκα", "δώδεκα" };
        public static string[] prefix = { "μείον ", "μηδέν ", " και " };
        public static string[] suffix = { "ο ", "α " };

        public static string[,] dm    = {{ "δέκα ", "ένδεκα ", "δώδεκα ", "δεκατρείς " },
                                         { "δέκα ", "ένδεκα ", "δώδεκα ", "δεκατρείς " },
                                         { "δέκα ", "ένδεκα ", "δώδεκα ", "δεκατρία " }};

        public static string[,] spc   = {{ "¢", "εκατό ", "εκατόν ", "χίλιοι ", "κις " },
                                         { "¢", "εκατό ", "εκατόν ", "χίλιες ", "κις " },
                                         { "¢", "εκατό ", "εκατόν ", "χίλια ", "κις " }};

        public static string[,] e     = {{ "", "εκατό", "διακόσιοι ", "τριακόσιοι ", "τετρακόσιοι ", "πεντακόσιοι ", "εξακόσιοι ", "επτακόσιοι ", "οκτακόσιοι ", "εννιακόσιοι " },
                                         { "", "εκατό", "διακόσιες ", "τριακόσιες ", "τετρακόσιες ", "πεντακόσιες ", "εξακόσιες ", "επτακόσιες ", "οκτακόσιες ", "εννιακόσιες " },
                                         { "", "εκατό", "διακόσια ", "τριακόσια ", "τετρακόσια ", "πεντακόσια ", "εξακόσια ", "επτακόσια ", "οκτακόσια ", "εννιακόσια " }};

        public static string[,] m     = {{ "", "ένας ", "δύο ", "τρεις ", "τέσσερις ", "πέντε ", "έξι ", "επτά ", "οκτώ ", "εννέα " },
                                         { "", "μία ",  "δύο ", "τρεις ", "τέσσερις ", "πέντε ", "έξι ", "επτά ", "οκτώ ", "εννέα " },
                                         { "", "ένα ",  "δύο ", "τρία ",  "τέσσερα ",  "πέντε ", "έξι ", "επτά ", "οκτώ ", "εννέα " }};
    }
}
