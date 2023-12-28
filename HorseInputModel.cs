using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAuction
{
    public class HorseInputModel
    {
        public Guid HorseId { get; private set; }
        public HorseInputModel()
        {
            HorseId = Guid.NewGuid();
        }

        [Required(ErrorMessage = "Registered Name is required")]
        [MaxLength(20, ErrorMessage = "Horses Name must be 20 Characters or less")]
        public string RegisteredName
        {
            get => registeredName;
            set => registeredName = CapitalizeEachWord(value);
        }
        private string registeredName = string.Empty;

        [Range(1, 30, ErrorMessage = "Age must be between 1 and 30")]
        public int Age { get; set; }

        public string Sex 
        {
            get => sex;
            set => sex = CapitalizeEachWord(value);
        }
        private string sex = string.Empty;

        public string Color
        {
            get => color;
            set => color = CapitalizeEachWord(value);
        }
        private string color = string.Empty;

        
        [MaxLength(250, ErrorMessage = "Description must be 250 characters or less")]
        public string Description 
        {
            get => description;
            set => description = CapitalizeSentences(value);
        }
        private string description;


        [EnumDataType(typeof(HorsePerformanceType), ErrorMessage = "Invalid Performance Type")]
        public string PerformanceType { get; set; }
        private string performanceType = string.Empty;

        private string CapitalizeEachWord(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    char[] letters = words[i].ToCharArray();
                    letters[0] = char.ToUpper(letters[0]);
                    words[i] = new string(letters);
                }
            }
            return string.Join(" ", words);
        }

        private string CapitalizeSentences(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string[] sentences = input.Split(new[] { ". " }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sentences.Length; i++)
            {
                sentences[i] = sentences[i].Trim();
                if (!string.IsNullOrEmpty(sentences[i]))
                {
                    char[] letters = sentences[i].ToCharArray();
                    letters[0] = char.ToUpper(letters[0]);
                    sentences[i] = new string(letters);
                }
            }
            return string.Join(". ", sentences);
        }
    }
}
