#define CMD_WAIT 0
#define CMD_KILL 1

typedef struct message_s {
	int command;
	int argument;
} message_t;