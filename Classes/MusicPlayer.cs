using NAudio.Midi;
using NAudio.Wave;
using System.Text.RegularExpressions;

namespace INF01120.Classes
{
    public class MusicPlayer
    {
        public List<string> InputNotesActions { get; set; }
        public MidiOut OutputDeviceMidi { get; set; } = new MidiOut(0);

        public MusicPlayer(List<string> InputNotesActions)
        {
            this.InputNotesActions = InputNotesActions;
        }
        static void PlayNoteWithInstrument(int instrument, int frequency, MidiOut outputDevice, int volume)
        {
            int channel = 1;
            outputDevice.Send(MidiMessage.ChangePatch(instrument, channel).RawData);
            outputDevice.Send(MidiMessage.StartNote(frequency, volume, 1).RawData);
            Thread.Sleep(500);
            outputDevice.Send(MidiMessage.StopNote(frequency, volume, 1).RawData);
        }
        public void SoundOut()
        {
            // Defining default settings
            int instrument = 1;
            int volume = 50;
            int note;
            int last_note_played;
            int octave = 1;
            const int LAST_OCTAVE = 7;
            const int A = 45;
            const int B = 47;
            const int C = 36;
            const int D = 38;
            const int E = 40;
            const int F = 41;
            const int G = 43;
            const int VOLUME_DEFAULT = 50;

            List<int> ValueOctaveVowels = new() { A, B, C, D, E, F, G };
            List<int> ValueVowels = new() { A, B, C, D, E, F, G };
            List<string> action_changers = new() { "Pause", "Raise Octave", "Double Volume" };
            List<string> instrument_changers = new() { "1+", "2+", "3+", "4+", "5+", "6+", "7+", "8+", "9+", "114", "7", "15", "76", "20" };

            using (var capture = new WasapiLoopbackCapture())
            {
                var writer = new WaveFileWriter("output.wav", capture.WaveFormat);

                capture.DataAvailable += (s, e) =>
                {
                    // Recording captured data
                    writer.Write(e.Buffer, 0, e.BytesRecorded);
                };

                capture.StartRecording();

                foreach (string NoteAction in InputNotesActions)
                {
                    if (action_changers.Contains(NoteAction))
                    {
                        if (NoteAction == "Pause")
                        {
                            Thread.Sleep(500); 
                        }
                        else if (NoteAction == "Raise Octave")
                            if (octave == LAST_OCTAVE) 
                            {
                                ValueOctaveVowels.Clear();
                                for (int index = 0; index < ValueVowels.Count; index++)
                                {
                                    ValueOctaveVowels.Add(ValueVowels[index]);
                                }
                                octave = 1;
                            }
                            else
                            {
                                for (int index = 0; index < ValueOctaveVowels.Count; index++)
                                {
                                    ValueOctaveVowels[index] += 12; // Raise Octave == Octave += 12
                                }
                                octave += 1;
                            }
                        // Double volume
                        else
                        {
                            if (volume > 63)
                            {
                                volume = VOLUME_DEFAULT; // default volume
                            }
                            else
                            {
                                volume *= 2;
                            }
                        }
                    }


                    // Change instrument
                    else if (instrument_changers.Contains(NoteAction))
                    {

                        if (NoteAction.Length == 2)
                        {
                            if (NoteAction[1] == '+') // se estamos lendo um numero par ou impar
                            {
                                instrument += Convert.ToInt32(NoteAction[0]);
                            }
                            else
                            {
                                instrument = Convert.ToInt32(NoteAction);
                            }

                        }
                        else
                        {
                            instrument = Convert.ToInt32(NoteAction);
                        }
                    }
                    // Play note
                    else
                    {
                        note = int.Parse(NoteAction);
                        PlayNoteWithInstrument(instrument, ValueOctaveVowels[note], OutputDeviceMidi, volume);
                        last_note_played = ValueOctaveVowels[note];
                    }
                }
                OutputDeviceMidi.Close();
                capture.StopRecording();
                writer.Close();
            }
        }
    }
}
