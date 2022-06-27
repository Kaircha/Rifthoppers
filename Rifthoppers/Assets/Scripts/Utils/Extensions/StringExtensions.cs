using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions {
  public static string Unknown(this string str) {
    string result = "";
    foreach (char c in str) {
      if (char.IsLetterOrDigit(c)) result += "?";
      else result += c;
    }
    return result;
  }
}