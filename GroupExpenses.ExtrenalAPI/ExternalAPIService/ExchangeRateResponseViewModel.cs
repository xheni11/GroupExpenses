
public class Rates
{
   public double USD { get; set; }
   public double AED { get; set; }
   public double ARS { get; set; }
   public double AUD { get; set; }
   public double CAD { get; set; }
   public double CHF { get; set; }
   public double CLP { get; set; }
   public double CNY { get; set; }
   public double EUR { get; set; }
   public double GBP { get; set; }
   public double HKD { get; set; }
}

public class ExchangeRatesResponse
{
   public string result { get; set; }
   public string provider { get; set; }
   public string documentation { get; set; }
   public string terms_of_use { get; set; }
   public int time_last_update_unix { get; set; }
   public string time_last_update_utc { get; set; }
   public int time_next_update_unix { get; set; }
   public string time_next_update_utc { get; set; }
   public int time_eol_unix { get; set; }
   public string base_code { get; set; }
   public Rates rates { get; set; }
}

