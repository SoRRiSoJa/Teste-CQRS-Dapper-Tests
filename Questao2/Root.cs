﻿namespace Questao2;

public class Root
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<Datum> data { get; set; }
}
