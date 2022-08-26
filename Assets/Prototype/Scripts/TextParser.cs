using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Scripts
{
   public class TextParser
   {
      private HashMap<string, EntityType> _keywords = new HashMap<string, EntityType>();

      public void AddKeyword(string keyword, EntityType type)
      {
         keyword = keyword.ToLower();
      
         if (_keywords.HasKey(keyword))
         {
            Debug.LogWarning("Keyword already defined in TextParser");
            return;
         }
      
         _keywords.Add(keyword.ToLower(), type);
      }

      public Command Parse(string input)
      {
         List<string> tokens = Tokenize(input);

         string action = "";
         string item = "";
         string _object = "";
         string conjunction = "";
         string item2 = "";
         string npc = "";
         
         foreach (string token in tokens)
         {
            if (_keywords.GetValue(token) == EntityType.ITEM && item == "")
            {
               item = token;
            }
            else if (_keywords.GetValue(token) == EntityType.ITEM && item != "" && item2 == "")
            {
               item2 = token;
            }
            else if (_keywords.GetValue(token) == EntityType.ACTION && action == "")
            {
               action = token;
            }
            else if (_keywords.GetValue(token) == EntityType.OBJECT && _object == "")
            {
               _object = token;
            }
            else if (_keywords.GetValue(token) == EntityType.CONJUNCTION && conjunction == "")
            {
               conjunction = token;
            }
            else if (_keywords.GetValue(token) == EntityType.NPC && npc == "")
            {
               npc = token;
            }
         }

         return new Command(action, item, _object, conjunction, item2, npc);
      }

      private List<string> Tokenize(string input)
      {
         List<string> tokens = new List<string>();
         List<string> keywords = _keywords.GetKeys(_keywords.Items);
         input = input.Trim().ToLower();
         
         foreach (string keyword in keywords)
         {
            if (input.IndexOf(keyword, StringComparison.Ordinal) >= 0)
            {
               tokens.Add(keyword);
            }
         }
      
         return tokens;
      }
   
   }

   public enum EntityType
   {
      ACTION,
      OBJECT,
      ITEM,
      CONJUNCTION,
      NPC
   }
}