#define LEDPORT PORTB    // Arduino pin 13 is bit 5 of port B
#define LEDPORT_DIR DDRB
#define LEDBIT 5 // pin 13
#define LEDBIT2 4 //pin 12


void setup() {  
 asm volatile (
      "sbi %[portdir], %[lbit]  \n"    // Set bit direction
      "sbi %[portdir], %[rbit]  \n"
      "3: "                                        // main loop label
      "   sbi %[port], %[lbit] \n"   
      "   cbi %[port], %[rbit] \n"  //  Turn on.
          /*
           * About the delay loop:
           *   The inner loop (dec, brne) is 3 cycles.
           *   For one second, we want 16million cycles, or 16000000/(3*256*256) loops.
           *   This is "about" 81.
           */
      "    clr r16  \n"
      "    clr r17  \n"
      "    ldi r18, 81  \n"   // 100 * 256
      "1:"  // 1st delay loop label
      "    dec r16  \n"
      "    brne 1b  \n"
      "    dec r17  \n"
      "    brne 1b  \n"
      "    dec r18  \n"
      "    brne 1b  \n"

      "    cbi   %[port], %[lbit] \n"       
      "    sbi   %[port], %[rbit] \n" // Turn off.

      "    clr r16  \n"
      "    clr r17  \n"
      "    ldi r18, 81  \n"
      "2:"  // 2nd delay loop label
      "    dec r16  \n"
      "    brne 2b  \n"
      "    dec r17  \n"
      "    brne 2b  \n"
      "    dec r18  \n"
      "    brne 2b  \n"

      "    rjmp 3b  \n"
      :
      : [portdir] "I" (_SFR_IO_ADDR(LEDPORT_DIR)),
        [port] "I" (_SFR_IO_ADDR(LEDPORT)),
        [lbit] "I" (LEDBIT),
        [rbit] "I" (LEDBIT2)
      );
}


void loop() {

}
